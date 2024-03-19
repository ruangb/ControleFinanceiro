using ControleFinanceiro.Domain.Manager.Interfaces;
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

        public int Insert(CreditCard entity)
        {
            return _baseRepository.Insert(entity);
        }

        public void Update(CreditCard entity)
        {
            _baseRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _baseRepository.Delete(id);
        }

    }
}
