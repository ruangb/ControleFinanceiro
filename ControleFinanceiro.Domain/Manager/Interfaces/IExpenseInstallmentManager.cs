﻿using ControleFinanceiro.Models;

namespace ControleFinanceiro.Domain.Manager.Interfaces
{
    public interface IExpenseInstallmentManager
    {
        IEnumerable<ExpenseInstallment> GetAll();
        IEnumerable<Expense> GetAllExpenses();
        IEnumerable<ExpenseInstallment> GetAllExpenseInstallmentsByBill(int billId, int idPerson, int idCreditCard, DateTime? startDueDate, bool onlyThirds);
        ExpenseInstallment GetById(int id);
        int Insert(ExpenseInstallment obj);
        void Update(ExpenseInstallment obj);
        void Delete(int id);
    }
}
