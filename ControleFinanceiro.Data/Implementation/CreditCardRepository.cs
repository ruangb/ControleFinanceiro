using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class CreditCardRepository : BaseRepository<CreditCard>, IBaseRepository<CreditCard>
    {
        public CreditCardRepository(IContext context) : base (context)
        {
        }

        public IEnumerable<CreditCard> GetAll()
        {
            return ExecuteGetAll();
        }

        public CreditCard GetById(int id)
        {
            return ExecuteGetById(id);
        }

        public void Insert(CreditCard entity)
        {
            ExecuteInsert(entity);
        }

        public void Update(CreditCard entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            ExecuteDelete(id);
        }
    }
}
