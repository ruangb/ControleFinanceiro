using ControleFinanceiro.Models;
using System.Data.SqlClient;

namespace ControleFinanceiro.Data.Interfaces
{
    public interface IExpenseInstallmentRepository
    {
        IEnumerable<ExpenseInstallment> GetAll();
        IEnumerable<Expense> GetAllExpenses();
        ExpenseInstallment GetById(int id);
        int Insert(ExpenseInstallment entity);
        void InsertByExpense(Expense entity, SqlCommand command);
        void Update(ExpenseInstallment entity);
        void Delete(int id);
    }
}
