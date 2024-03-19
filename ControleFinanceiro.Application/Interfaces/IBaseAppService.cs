using ControleFinanceiro.CrossCutting;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IBaseAppService<T> where T : class
    {
        AppServiceResult<IEnumerable<T>> GetAll();
        AppServiceResult<T> GetById(int id);
        AppServiceResult<int> Insert(T obj);
        AppServiceBaseResult Update(T obj);
        AppServiceBaseResult Delete(int id);
    }
}
