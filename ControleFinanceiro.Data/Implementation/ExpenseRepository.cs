using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Transactions;
using System.Text.Json.Serialization;
using System.Text.Json;

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

                var sql = @"SELECT exp.Id, exp.IdPerson, exp.IdCreditCard, exp.Status, exp.OperationDate, exp.Description, exp.Amount, 
                                   exp.ParcelQuantity, per.Id perId, per.Name, per.Main, per.Inactive, cre.Id creId, cre.Name, cre.DueDay, cre.ClosingDays, 
                                   cre.Inactive, ei.Id eiId, ei.IdExpense, ei.IdBill, ei.Installment, ei.Status, ei.ReferenceDate, ei.Value 
                          FROM Expense (NOLOCK) exp
                          INNER JOIN Person per ON exp.IdPerson = per.Id
                          LEFT JOIN CreditCard cre ON exp.IdCreditCard = cre.Id
                          INNER JOIN ExpenseInstallment ei ON exp.Id = ei.IdExpense";

                var expenses = conn.Query<Expense, Person, CreditCard, ExpenseInstallment, Expense>(sql, (expense, person, creditCard, expenseInstallment) => {
                    expense.Person = person;
                    expense.CreditCard = creditCard;
                    expense.ExpenseInstallments = [expenseInstallment];
                    return expense;
                }, splitOn: "Id, perId, creId, eiId");

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
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                List<ExpenseInstallment> lstExpenseInstallments = [];

                conn.Open();
                var sql = @$"SELECT exp.Id, exp.IdPerson, exp.IdCreditCard, exp.Status, exp.OperationDate, exp.Description, exp.Amount, 
                                    exp.ParcelQuantity, per.Id perId, per.Name, per.Main, per.Inactive, cre.Id creId, cre.Name, cre.DueDay, cre.ClosingDays, 
                                    cre.Inactive, ei.Id eiId, ei.IdExpense, ei.IdBill, ei.Installment, ei.Status, ei.ReferenceDate, ei.Value 
                          FROM Expense (NOLOCK) exp
                          INNER JOIN Person per ON exp.IdPerson = per.Id
                          LEFT JOIN CreditCard cre ON exp.IdCreditCard = cre.Id
                          INNER JOIN ExpenseInstallment ei ON exp.Id = ei.IdExpense
                          WHERE exp.Id = {id}";

                var expenses = conn.Query<Expense, Person, CreditCard, ExpenseInstallment, Expense>(sql, (expense, person, creditCard, expenseInstallment) => {
                    expense.Person = person;
                    expense.CreditCard = creditCard;
                    lstExpenseInstallments.Add(expenseInstallment);
                    return expense;
                }, splitOn: "Id, perId, creId, eiId");

                foreach (var exp in expenses)
                {
                    exp.ExpenseInstallments = lstExpenseInstallments;
                }

                return expenses.FirstOrDefault();
            }
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
