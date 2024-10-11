using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using System.Data.SqlClient;
using Dapper;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        private readonly IContext _context;

        public BillRepository(IContext context) : base(context) 
        {
            _context = context;
        }

        public IEnumerable<Bill> GetAll()
        {
            return ExecuteGetAll();
        }

        public IEnumerable<Bill> GetAllBills(int idPerson, int idCreditCard, DateTime? startDueDate, bool onlyThirds)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                string filterPerson = string.Empty;
                string filterCard = string.Empty;
                string filterDate = string.Empty;

                conn.Open();

                if (onlyThirds || idPerson > 0)
                {
                    filterPerson = @" INNER JOIN Expense (NOLOCK) e ON ei.IdExpense = e.Id 
                                      INNER JOIN Person (NOLOCK) p ON e.IdPerson = p.Id ";

                    if (onlyThirds)
                        filterPerson += " WHERE p.Main = 0 ";

                    if (idPerson > 0)
                        filterPerson += $" {(onlyThirds ? "AND" : "WHERE")} p.Id = {idPerson}";
                }

                if (idCreditCard > 0)
                    filterCard = $" {(!string.IsNullOrEmpty(filterPerson) ? "AND" : "WHERE")} c.Id = {idCreditCard} ";

                if (startDueDate != null)
                    filterDate = $" {(!string.IsNullOrEmpty(filterPerson) || !string.IsNullOrEmpty(filterCard) ? "AND" : "WHERE")} b.DueDate >= '{startDueDate.GetValueOrDefault():yyyy-MM-dd}'";

                var sql = @$"SELECT b.Id, b.IdCreditCard, b.DueDate, SUM(ei.Value) Value, c.[Name] FROM ExpenseInstallment (NOLOCK) ei
                             INNER JOIN Bill (NOLOCK) b ON ei.IdBill = b.Id
                             INNER JOIN CreditCard (NOLOCK) c ON b.IdCreditCard = c.Id
                             {filterPerson}
                             {filterCard}
                             {filterDate}
                             GROUP BY b.Id, b.IdCreditCard, b.DueDate, c.[Name]
                             ORDER BY b.DueDate ASC";

                var bills = conn.Query<Bill, CreditCard, Bill>(sql, (bill, creditCard) => {
                    bill.CreditCard = creditCard;
                    return bill;
                }, splitOn: "Name");

                return bills;
            }
        }

        public Bill GetById(int id)
        {
            return ExecuteGetById(id);
        }

        public int Insert(Bill entity)
        {
            return ExecuteInsert(entity);
        }

        public void Update(Bill entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            ExecuteDelete(id);
        }
    }
}
