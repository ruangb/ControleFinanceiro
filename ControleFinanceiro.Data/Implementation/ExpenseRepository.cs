using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class ExpenseRepository : BaseRepository<Expense>, IBaseRepository<Expense>
    {
        private readonly IContext _context;
        private readonly IExpenseInstallmentRepository _expenseInstallment;

        public ExpenseRepository(IContext context, IExpenseInstallmentRepository expenseInstallment) : base (context)
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

        public Expense GetById(int id)
        {
            return ExecuteGetById(id);
        }

        public int Insert(Expense entity)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    using (var command = conn.CreateCommand())
                    {
                        List<string> fieldNames = [];
                        DataSupport<Expense>.SetCommandParametersForInsertByEntityValues(entity, command, fieldNames);
                        string sqlInsertExpense = DataSupport<Expense>.GenerateSqlInsert(fieldNames);

                        command.CommandText = sqlInsertExpense;
                        entity.Id = Convert.ToInt32(command.ExecuteScalar() as int?);

                        _expenseInstallment.InsertByExpense(entity, command);

                        tran.Commit();

                        return entity.Id;
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
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
