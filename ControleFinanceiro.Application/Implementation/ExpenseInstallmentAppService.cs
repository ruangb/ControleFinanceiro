using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Application.Implementation
{
    public sealed class ExpenseInstallmentAppService : IExpenseInstallmentAppService
    {
        private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;
        private readonly IMapper _mapper;

        public ExpenseInstallmentAppService(IExpenseInstallmentRepository expenseInstallmentRepository, IMapper mapper)
        {
            _expenseInstallmentRepository = expenseInstallmentRepository;
            _mapper = mapper;
        }

        public AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> GetAll()
        {
            AppServiceResult<IEnumerable<ExpenseInstallmentDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<ExpenseInstallmentDTO>>(_expenseInstallmentRepository.GetAll()));
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
                result.BuildSucessResult(_mapper.Map<IEnumerable<ExpenseDTO>>(_expenseInstallmentRepository.GetAllExpenses()));
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
                result.BuildSucessResult(_mapper.Map<ExpenseInstallmentDTO>(_expenseInstallmentRepository.GetById(id)));
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
                result.BuildSucessResult(_expenseInstallmentRepository.Insert(entity));
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
                _expenseInstallmentRepository.Update(entity);
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
                _expenseInstallmentRepository.Delete(id);
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
