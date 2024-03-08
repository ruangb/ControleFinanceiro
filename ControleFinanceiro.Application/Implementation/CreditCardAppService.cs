using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Application.Implementation
{
    public sealed class CreditCardAppService : IBaseAppService<CreditCardDTO>
    {
        private readonly IBaseManager<CreditCard> _baseManager;
        private IMapper _mapper;

        public CreditCardAppService(IBaseManager<CreditCard> baseManager, IMapper mapper)
        {
            _baseManager = baseManager;
            _mapper = mapper;
        }

        public IEnumerable<CreditCardDTO> GetAll()
        {
            var dto = _mapper.Map<IEnumerable<CreditCardDTO>>(_baseManager.GetAll());
            return dto;
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

        public void Update(CreditCardDTO dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _baseManager.Delete(id);
        }
    }
}
