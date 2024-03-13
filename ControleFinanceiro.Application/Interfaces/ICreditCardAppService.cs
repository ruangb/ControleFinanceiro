using ControleFinanceiro.CrossCutting;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IBaseAppService<T> where T : class
    {
        AppServiceResult<IEnumerable<T>> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
    }
}
