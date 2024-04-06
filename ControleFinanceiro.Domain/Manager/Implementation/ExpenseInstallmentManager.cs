using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Domain.Manager.Implementation
{
    public sealed class ExpenseInstallmentManager : IExpenseInstallmentManager
    {
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;

        public ExpenseInstallmentManager(IExpenseInstallmentRepository expenseInstallmentRepository)
        {
            _expenseInstallmentRepository = expenseInstallmentRepository;
        }

        public IEnumerable<ExpenseInstallment> GetAll()
        {
            return _expenseInstallmentRepository.GetAll();
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            return _expenseInstallmentRepository.GetAllExpenses();
        }

        public ExpenseInstallment GetById(int id)
        {
            return _expenseInstallmentRepository.GetById(id);
        }

        public int Insert(ExpenseInstallment entity)
        {
            return _expenseInstallmentRepository.Insert(entity);
        }

        public void Update(ExpenseInstallment entity)
        {
            _expenseInstallmentRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _expenseInstallmentRepository.Delete(id);
        }

    }
}
