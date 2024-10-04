using ControleFinanceiro.Models;

namespace ControleFinanceiro.Data.Interfaces
{
    public interface IBillRepository
    {
        IEnumerable<Bill> GetAll();
        IEnumerable<Bill> GetAllBills(int idPerson, bool onlyThirds);
        Bill GetById(int id);
        int Insert(Bill entity);
        void Update(Bill entity);
        void Delete(int id);
    }
}
