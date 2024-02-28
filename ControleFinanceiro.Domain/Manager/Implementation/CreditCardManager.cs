using ControleFinanceiro.Domain.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Domain.Manager.Implementation
{
    public sealed class CreditCardManager : IBaseManager<CreditCard>
    {
        private readonly IBaseRepository<CreditCard> _baseRepository;

        public CreditCardManager(IBaseRepository<CreditCard> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public IEnumerable<CreditCard> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public CreditCard GetById(int id)
        {
            return _baseRepository.GetById(id);
        }

        public void Insert(CreditCard obj)
        {
            _baseRepository.Insert(obj);
        }

        public void Update(CreditCard obj)
        {
            _baseRepository.Update(obj);
        }

    }
}
