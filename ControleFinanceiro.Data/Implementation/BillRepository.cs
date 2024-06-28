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

        public IEnumerable<Bill> GetAllBills()
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();

                var sql = @"SELECT b.Id, b.IdCreditCard, b.DueDate, SUM(ei.Value) Value, c.[Name] FROM ExpenseInstallment (NOLOCK) ei
                            INNER JOIN Bill (NOLOCK) b ON ei.IdBill = b.Id
                            INNER JOIN CreditCard (NOLOCK) c ON b.IdCreditCard = c.Id
                            GROUP BY b.Id, b.IdCreditCard, b.DueDate, c.[Name]";

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
