using ControleFinanceiro.Domain;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IBaseAppService<T> where T : class
    {     
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj);
    }
}
