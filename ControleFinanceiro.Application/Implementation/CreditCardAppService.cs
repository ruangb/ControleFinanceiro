using AutoMapper;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Domain;
using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var creditCards = _mapper.Map<IEnumerable<CreditCardDTO>>(_baseManager.GetAll());

            return creditCards;
        }

        public CreditCardDTO GetById(int id)
        {
            throw new NotImplementedException();
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
    }
}
