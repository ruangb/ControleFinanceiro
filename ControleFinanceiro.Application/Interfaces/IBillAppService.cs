using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IBillAppService
    {
        AppServiceResult<IEnumerable<BillDTO>> GetAll();
        AppServiceResult<IEnumerable<BillDTO>> GetAllBills(ExpenseDTO dto);
        AppServiceResult<BillDTO> GetById(int id);
        AppServiceResult<int> Insert(BillDTO obj);
        AppServiceBaseResult Update(BillDTO obj);
        AppServiceBaseResult Delete(int id);
    }
}
