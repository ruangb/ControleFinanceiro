using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Domain.Manager.Implementation
{
    public sealed class BillManager : IBillManager
    {
        private readonly IBillRepository _BillRepository;

        public BillManager(IBillRepository BillRepository)
        {
            _BillRepository = BillRepository;
        }

        public IEnumerable<Bill> GetAll()
        {
            return _BillRepository.GetAll();
        }

        public IEnumerable<Bill> GetAllBills(int idPerson, bool onlyThirds)
        {
            return _BillRepository.GetAllBills(idPerson, onlyThirds);
        }

        public Bill GetById(int id)
        {
            return _BillRepository.GetById(id);
        }

        public int Insert(Bill entity)
        {
            return _BillRepository.Insert(entity);
        }

        public void Update(Bill entity)
        {
            _BillRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _BillRepository.Delete(id);
        }

    }
}
