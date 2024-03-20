using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Application.Implementation
{
    public sealed class ExpenseAppService : IBaseAppService<ExpenseDTO>
    {
        private readonly IBaseManager<Expense> _baseManager;
        private readonly IMapper _mapper;

        public ExpenseAppService(IBaseManager<Expense> baseManager, IMapper mapper)
        {
            _baseManager = baseManager;
            _mapper = mapper;
        }

        public AppServiceResult<IEnumerable<ExpenseDTO>> GetAll()
        {
            AppServiceResult<IEnumerable<ExpenseDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<ExpenseDTO>>(_baseManager.GetAll()));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<ExpenseDTO> GetById(int id)
        {
            AppServiceResult<ExpenseDTO> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<ExpenseDTO>(_baseManager.GetById(id)));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<int> Insert(ExpenseDTO dto)
        {
            AppServiceResult<int> result = new();

            try
            {
                var entity = _mapper.Map<Expense>(dto);
                result.BuildSucessResult(_baseManager.Insert(entity));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceBaseResult Update(ExpenseDTO dto)
        {
            AppServiceBaseResult result = new();

            try
            {
                var entity = _mapper.Map<Expense>(dto);
                _baseManager.Update(entity);
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
                _baseManager.Delete(id);
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
