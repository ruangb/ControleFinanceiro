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

        //public int Insert(Expense entity)
        //{
        //    using (var conn = new SqlConnection(_context.GetConnectionString()))
        //    {
        //        conn.Open();
        //        SqlTransaction tran = conn.BeginTransaction();

        //        try
        //        {
        //            using (var command = conn.CreateCommand())
        //            {
        //                List<string> fieldNames = [];
        //                DataSupport<Expense>.SetCommandParametersForInsertByEntityValues(entity, command, fieldNames);
        //                string sqlInsertExpense = DataSupport<Expense>.GenerateSqlInsert(fieldNames);

        //                command.CommandText = sqlInsertExpense;
        //                command.Transaction = tran;
        //                SqlDataReader reader = command.ExecuteReader();
  
        //                while (reader.Read())
        //                {
        //                    entity.Id = reader.GetInt32(0);
        //                }

        //                _expenseInstallment.InsertByExpense(entity, command);

        //                tran.Commit();

        //                return entity.Id;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            tran.Rollback();
        //            throw;
        //        }
        //    }
        //}

        public int Insert(Expense entity)
        {
            int id;

            using (var tranScope = new TransactionScope())
            {
                using (var conn = new SqlConnection(_context.GetConnectionString()))
                {
                    conn.Open();
                    //SqlTransaction tran = conn.BeginTransaction();

                    using (var command = conn.CreateCommand())
                    {
                        List<string> fieldNames = [];

                        DataSupport<Expense>.SetCommandParametersForInsertByEntityValues(entity, command, fieldNames);

                        command.CommandText = DataSupport<Expense>.GenerateSqlInsert(fieldNames);

                        id = (int)command.ExecuteScalar();
                        //tran.Commit();
                    }

                    decimal parcelValue = entity.Amount / entity.ParcelQuantity;

                    for (int i = 1; i <= entity.ParcelQuantity; i++)
                    {
                        var result = conn.ExecuteReader("PRC_SAVE_EXPENSE_INSTALLMENT",
                            new
                            {
                                N_ID = 0,
                                N_ID_CREDIT_CARD = entity.IdCreditCard,
                                N_ID_EXPENSE = id,
                                N_INSTALLMENT = i,
                                S_STATUS = entity.Status,
                                D_REFERENCE_DATE = entity.OperationDate.AddMonths(i - 1),
                                N_VALUE = parcelValue,
                                RESULT = "",
                                RESULT_MESS = 0,
                            }, commandType: CommandType.StoredProcedure);
                    }

                    tranScope.Complete();
                }
            }

            return id;
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
