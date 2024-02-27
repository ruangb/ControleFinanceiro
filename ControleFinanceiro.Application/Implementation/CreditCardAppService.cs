using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Application.Implementation
{
    internal class CreditCardAppService : IBaseAppService<CreditCard>
    {
        public IEnumerable<CreditCard> GetAll()
        {
            throw new NotImplementedException();
        }

        public CreditCard GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(CreditCard obj)
        {
            throw new NotImplementedException();
        }

        public void Update(CreditCard obj)
        {
            throw new NotImplementedException();
        }
    }
}
