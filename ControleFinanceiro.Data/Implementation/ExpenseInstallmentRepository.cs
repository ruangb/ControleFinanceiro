using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using System.Data.SqlClient;
using ControleFinanceiro.CrossCutting.Utilities;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class ExpenseInstallmentRepository : BaseRepository<ExpenseInstallment>, IExpenseInstallmentRepository
    {
        public ExpenseInstallmentRepository(IContext context) : base(context) 
        {
        }

        public IEnumerable<ExpenseInstallment> GetAll()
        {
            return ExecuteGetAll();
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
