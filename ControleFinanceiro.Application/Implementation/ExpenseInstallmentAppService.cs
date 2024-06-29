using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Application.Implementation
{
    public sealed class ExpenseInstallmentAppService : IExpenseInstallmentAppService
    {
        private readonly IExpenseInstallmentManager _expenseInstallmentManager;
        private readonly IMapper _mapper;

        public ExpenseInstallmentAppService(IExpenseInstallmentManager expenseInstallmentManager, IMapper mapper)
        {
            _expenseInstallmentManager = expenseInstallmentManager;
            _mapper = mapper;
        }

        public AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> GetAll()
        {
            AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<ExpenseInstallmentDTO>>(_expenseInstallmentManager.GetAll()));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<IEnumerable<ExpenseDTO>> GetAllExpenses()
        {
            AppServiceResult<IEnumerable<ExpenseDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<ExpenseDTO>>(_expenseInstallmentManager.GetAllExpenses()));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> GetAllExpenseInstallmentsByBill(int billId)
        {
            AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<ExpenseInstallmentDTO>>(_expenseInstallmentManager.GetAllExpenseInstallmentsByBill(billId)));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<ExpenseInstallmentDTO> GetById(int id)
        {
            AppServiceResult<ExpenseInstallmentDTO> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<ExpenseInstallmentDTO>(_expenseInstallmentManager.GetById(id)));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<int> Insert(ExpenseInstallmentDTO dto)
        {
            AppServiceResult<int> result = new();

            try
            {
                var entity = _mapper.Map<ExpenseInstallment>(dto);
                result.BuildSucessResult(_expenseInstallmentManager.Insert(entity));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceBaseResult Update(ExpenseInstallmentDTO dto)
        {
            AppServiceBaseResult result = new();

            try
            {
                var entity = _mapper.Map<ExpenseInstallment>(dto);
                _expenseInstallmentManager.Update(entity);
                result.BuildSucessResult();
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceBaseResult Delete(int id)
        {
            AppServiceBaseResult result = new();

            try
            {
                _expenseInstallmentManager.Delete(id);
                result.BuildSucessResult();
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }
    }
}
