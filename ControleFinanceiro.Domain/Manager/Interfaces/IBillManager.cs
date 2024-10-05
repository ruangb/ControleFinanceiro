using ControleFinanceiro.Models;

namespace ControleFinanceiro.Domain.Manager.Interfaces
{
    public interface IBillManager
    {
        IEnumerable<Bill> GetAll();
        IEnumerable<Bill> GetAllBills(int idPerson, int idCreditCard, DateTime? startDueDate, bool onlyThirds);
        Bill GetById(int id);
        int Insert(Bill obj);
        void Update(Bill obj);
        void Delete(int id);
    }
}
