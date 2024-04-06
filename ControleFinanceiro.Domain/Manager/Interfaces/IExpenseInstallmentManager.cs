using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Domain.Manager.Interfaces
{
    public interface IExpenseInstallmentManager
    {
        IEnumerable<ExpenseInstallment> GetAll();
        IEnumerable<Expense> GetAllExpenses();
        ExpenseInstallment GetById(int id);
        int Insert(ExpenseInstallment obj);
        void Update(ExpenseInstallment obj);
        void Delete(int id);
    }
}
