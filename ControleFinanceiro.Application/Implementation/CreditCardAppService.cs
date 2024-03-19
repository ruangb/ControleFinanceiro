using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Application.Implementation
{
    public sealed class CreditCardAppService : IBaseAppService<CreditCardDTO>
    {
        private readonly IBaseManager<CreditCard> _baseManager;
        private readonly IMapper _mapper;

        public CreditCardAppService(IBaseManager<CreditCard> baseManager, IMapper mapper)
        {
            _baseManager = baseManager;
            _mapper = mapper;
        }

        public AppServiceResult<IEnumerable<CreditCardDTO>> GetAll()
        {
            AppServiceResult<IEnumerable<CreditCardDTO>> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<IEnumerable<CreditCardDTO>>(_baseManager.GetAll()));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<CreditCardDTO> GetById(int id)
        {
            AppServiceResult<CreditCardDTO> result = new();

            try
            {
                result.BuildSucessResult(_mapper.Map<CreditCardDTO>(_baseManager.GetById(id)));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceResult<int> Insert(CreditCardDTO dto)
        {
            AppServiceResult<int> result = new();

            try
            {
                var entity = _mapper.Map<CreditCard>(dto);
                result.BuildSucessResult(_baseManager.Insert(entity));
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public AppServiceBaseResult Update(CreditCardDTO dto)
        {
            AppServiceBaseResult result = new();

            try
            {
                var entity = _mapper.Map<CreditCard>(dto);
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
