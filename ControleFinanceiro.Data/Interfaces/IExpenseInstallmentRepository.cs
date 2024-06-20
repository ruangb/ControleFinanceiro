using ControleFinanceiro.Models;
using System.Data.SqlClient;

namespace ControleFinanceiro.Data.Interfaces
{
    public interface IExpenseInstallmentRepository
    {
        IEnumerable<ExpenseInstallment> GetAll();
        IEnumerable<Expense> GetAllExpenses();
        ExpenseInstallment GetById(int id);
        IEnumerable<ExpenseInstallment> GetByExpenseId(int expenseId, SqlConnection conn);
        int Insert(ExpenseInstallment entity);
        void Update(ExpenseInstallment entity);
        void Delete(int id);
    }
}
