using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using System.Data.SqlClient;
using ControleFinanceiro.CrossCutting.Utilities;
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

        public ExpenseInstallment GetById(int id)
        {
            return ExecuteGetById(id);
        }

        public int Insert(ExpenseInstallment entity)
        {
            return ExecuteInsert(entity);
        }

        public void InsertByExpense(Expense expense, SqlCommand command)
        {
            string sqlValuesInsertExpenseInstallment = string.Empty;
            List<string> fieldNames = [];

            command.Parameters.Clear();

            decimal installmentValue = expense.Amount / expense.ParcelQuantity;

            for (int i = 1; i <= expense.ParcelQuantity; i++)
            {
                List<string> parameters = [];
                fieldNames = [];
                DateTime dueDate = expense.OperationDate;

                if (i > 1)
                    dueDate = dueDate.AddDays(30 * (i -1));

                DataSupport<ExpenseInstallment>
                    .SetCommandParametersForBulkInsertByEntityValues(
                    new ExpenseInstallment(expense.Id,
                    (short)i,
                    expense.Status,
                    dueDate,
                    installmentValue), command, fieldNames, parameters, false, i - 1);

                sqlValuesInsertExpenseInstallment += $@"({parameters.Parameterize()}),";
            }

            sqlValuesInsertExpenseInstallment = sqlValuesInsertExpenseInstallment.Remove(sqlValuesInsertExpenseInstallment.Length - 1);

            command.CommandText = $@"INSERT INTO {typeof(ExpenseInstallment).Name} ({fieldNames.Parameterize().Replace("@", "")}) 
                                     VALUES {sqlValuesInsertExpenseInstallment}";

            command.ExecuteNonQuery();
        }

        public void Update(ExpenseInstallment entity)
        {
            //ExecuteUpdate(entity.Id, entity);
        }

        public void Delete(int id)
        {
            ExecuteDelete(id);
        }
    }
}
