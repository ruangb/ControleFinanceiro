using ControleFinanceiro.Application.Interfaces;
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
    public sealed class CreditCardAppService : IBaseAppService<CreditCard>
    {
        private readonly IBaseManager<CreditCard> _baseManager;

        public CreditCardAppService(IBaseManager<CreditCard> baseManager)
        {
            _baseManager = baseManager;
        }

        public  IEnumerable<CreditCard> GetAll()
        {
            return _baseManager.GetAll();
        }

        public  CreditCard GetById(int id)
        {
            throw new NotImplementedException();
        }

        public  void Insert(CreditCard obj)
        {
            throw new NotImplementedException();
        }

        public  void Update(CreditCard obj)
        {
            throw new NotImplementedException();
        }
    }
}
