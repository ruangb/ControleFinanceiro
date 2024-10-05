using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IExpenseInstallmentAppService
    {
        AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> GetAll();
        AppServiceResult<IEnumerable<ExpenseDTO>> GetAllExpenses();
        AppServiceResult<ExpenseInstallmentDTO> GetById(int id);
        AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> GetAllExpenseInstallmentsByBill(BillDTO obj);
        AppServiceResult<int> Insert(ExpenseInstallmentDTO obj);
        AppServiceBaseResult Update(ExpenseInstallmentDTO obj);
        AppServiceBaseResult Delete(int id);
    }
}
