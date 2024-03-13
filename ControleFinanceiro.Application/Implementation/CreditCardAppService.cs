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

        public CreditCardDTO GetById(int id)
        {
            var dto = _mapper.Map<CreditCardDTO>(_baseManager.GetById(id));
            return dto;
        }

        public void Insert(CreditCardDTO dto)
        {
            var entity = _mapper.Map<CreditCard>(dto);
            _baseManager.Insert(entity);
        }

        public AppServiceResult<CreditCardDTO> Update(CreditCardDTO dto)
        {
            AppServiceResult<CreditCardDTO> result = new();

            try
            {
                var entity = _mapper.Map<CreditCard>(dto);
                _baseManager.Update(entity);
                result.BuildSucessResult(dto);
            }
            catch (Exception ex)
            {
                result.BuildErrorResult(ex);
            }

            return result;
        }

        public void Delete(int id)
        {
            _baseManager.Delete(id);
        }
    }
}
