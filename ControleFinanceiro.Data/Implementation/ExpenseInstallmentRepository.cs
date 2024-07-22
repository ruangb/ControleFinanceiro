using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using System.Data.SqlClient;
using Dapper;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class ExpenseInstallmentRepository : BaseRepository<ExpenseInstallment>, IExpenseInstallmentRepository
    {
        private readonly IContext _context;

        public ExpenseInstallmentRepository(IContext context) : base(context) 
        {
            _context = context;
        }

        public IEnumerable<ExpenseInstallment> GetAll()
        {
            return ExecuteGetAll();
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();

                var sql = @"SELECT * FROM Expense (NOLOCK) exp
                          INNER JOIN Person per ON exp.IdPerson = per.Id
                          LEFT JOIN CreditCard cre ON exp.IdCreditCard = cre.Id
                          INNER JOIN ExpenseInstallment ei ON exp.Id = ei.IdExpense";

                var expenses = conn.Query<Expense, Person, CreditCard, ExpenseInstallment, Expense>(sql, (expense, person, creditCard, expenseInstallment) => {
                    expense.Person = person;
                    expense.CreditCard = creditCard;
                    expense.ExpenseInstallments = [expenseInstallment];
                    return expense;
                }, splitOn: "Id, Id, Id, IdExpense");

                var result = expenses.GroupBy(e => e.Id).Select(g =>
                {
                    var exp = g.First();
                    exp.ExpenseInstallments = g.Select(e => e.ExpenseInstallments.Single()).ToList();
                    return exp;
                });

                return result.ToList();
            }
        }

        public IEnumerable<ExpenseInstallment> GetAllExpenseInstallmentsByBill(int billId)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();

                var sql = @$"SELECT ei.*, e.*, c.Name, p.Name FROM ExpenseInstallment (NOLOCK) ei
                             INNER JOIN Bill (NOLOCK) b ON ei.IdBill = b.Id
                             INNER JOIN CreditCard (NOLOCK) c ON b.IdCreditCard = c.Id
                             INNER JOIN Expense (NOLOCK) e ON ei.IdExpense = e.Id
                             INNER JOIN Person (NOLOCK) p ON e.IdPerson = p.Id
                             WHERE b.Id = {billId}";

                var expenseInstallments = conn.Query<ExpenseInstallment, Expense, CreditCard, Person, ExpenseInstallment>(sql, (expenseInsallment, expense, creditCard, person) => {
                    expenseInsallment.Expense = expense;
                    expenseInsallment.Expense.CreditCard = creditCard;
                    expenseInsallment.Expense.Person = person;
                    return expenseInsallment;
                }, splitOn: "Id, Name, Name");

                return expenseInstallments;
            }
        }

        public ExpenseInstallment GetById(int id)
        {
            return ExecuteGetById(id);
        }

        public IEnumerable<ExpenseInstallment> GetByExpenseId(int expenseId, SqlConnection conn)
        {
            return conn.Query<ExpenseInstallment>($"SELECT * FROM ExpenseInstallment (NOLOCK) WHERE IdExpense = {expenseId}");
        }

        public int Insert(ExpenseInstallment entity)
        {
            return ExecuteInsert(entity);
        }

        public void Update(ExpenseInstallment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            ExecuteDelete(id);
        }
    }
}
