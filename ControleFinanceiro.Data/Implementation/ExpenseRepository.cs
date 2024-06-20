using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Transactions;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class ExpenseRepository : BaseRepository<Expense>, IBaseRepository<Expense>
    {
        private readonly IContext _context;
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;

        public ExpenseRepository(IContext context, IExpenseInstallmentRepository expenseInstallmentRepository) : base (context)
        {
            _context = context;
            _expenseInstallmentRepository = expenseInstallmentRepository;
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
            int id;

            using (var tranScope = new TransactionScope())
            {
                using (var conn = new SqlConnection(_context.GetConnectionString()))
                {
                    conn.Open();

                    using (var command = conn.CreateCommand())
                    {
                        List<string> fieldNames = [];

                        DataSupport<Expense>.SetCommandParametersForInsertByEntityValues(entity, command, fieldNames);

                        command.CommandText = DataSupport<Expense>.GenerateSqlInsert(fieldNames);

                        id = (int)command.ExecuteScalar();
                    }

                    ExecuteProcSaveInstallment(id, entity, conn);

                    tranScope.Complete();
                }
            }

            return id;
        }

        public void Update(Expense entity)
        {
            using (var tranScope = new TransactionScope())
            {
                ExecuteUpdate(entity.Id, entity);

                using (var conn = new SqlConnection(_context.GetConnectionString()))
                {
                    conn.Open();

                    ExecuteProcSaveInstallment(entity.Id, entity, conn, true);
                }
                
                tranScope.Complete();
            }
        }

        public void Delete(int id)
        {
            ExecuteDelete(id);
        }

        private void ExecuteProcSaveInstallment(int idExpense, Expense entity, SqlConnection conn, bool isUpdate = false)
        {
            decimal parcelValue = entity.Amount / entity.ParcelQuantity;
            IEnumerable<ExpenseInstallment> installments = [];

            if (isUpdate)
                installments = _expenseInstallmentRepository.GetByExpenseId(entity.Id, conn);

            for (int i = 1; i <= entity.ParcelQuantity; i++)
            {
                int id = 0;

                if (isUpdate)
                    id = installments.Where(x => x.Installment == i).Select(x => x.Id).FirstOrDefault();

                conn.ExecuteReader("PRC_SAVE_EXPENSE_INSTALLMENT",
                    new
                    {
                        N_ID = id,
                        N_ID_CREDIT_CARD = entity.IdCreditCard,
                        N_ID_EXPENSE = idExpense,
                        N_INSTALLMENT = i,
                        S_STATUS = entity.Status,
                        D_REFERENCE_DATE = entity.OperationDate.AddMonths(i - 1),
                        N_VALUE = parcelValue,
                        RESULT = "",
                        RESULT_MESS = 0,
                    }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
