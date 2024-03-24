using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using System.Data.SqlClient;
using Dapper;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class ExpenseRepository : BaseRepository<Expense>, IBaseRepository<Expense>
    {
        private readonly IContext _context;
        private readonly IBaseRepository<ExpenseInstallment> _expenseInstallment;

        public ExpenseRepository(IContext context, IBaseRepository<ExpenseInstallment> expenseInstallment) : base (context)
        {
            _context = context;
            _expenseInstallment = expenseInstallment;
        }

        public IEnumerable<Expense> GetAll()
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();

                var sql = @"SELECT * FROM Expense (NOLOCK) exp
                          INNER JOIN Person per ON exp.IdPerson = per.Id
                          LEFT JOIN CreditCard cre ON exp.IdCreditCard = cre.Id";

                var expenses = conn.Query<Expense, Person, CreditCard, Expense>(sql, (expense, person, creditCard) => {
                    expense.Person = person;
                    expense.CreditCard = creditCard;
                    return expense;
                });

                return expenses;
            }
        }

        public Expense GetById(int id)
        {
            return ExecuteGetById(id);
        }

        public int Insert(Expense entity)
        {
            return ExecuteInsert(entity);

            //ExecuteInsert(entity);

            //List<ExpenseInstallment> installments = new List<ExpenseInstallment>();
            //decimal installmentValue = entity.Amount / entity.ParcelQuantity;

            //for (int i = 1; i <= entity.ParcelQuantity; i++)
            //{
            //    DateTime dueDate = entity.OperationDate;

            //    if (i > 1)
            //        dueDate.AddDays(30 * i);

            //    installments.Add(new ExpenseInstallment(entity.Id, (short)i, entity.Status, dueDate, installmentValue));
            //}

            //_expenseInstallment.Insert(entity);

            //ExecuteInsert(installments);
        }

        public void Update(Expense entity)
        {
            ExecuteUpdate(entity.Id, entity);
        }

        public void Delete(int id)
        {
            ExecuteDelete(id);
        }
    }
}
