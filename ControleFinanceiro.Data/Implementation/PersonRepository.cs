using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class PersonRepository : BaseRepository<Person>, IBaseRepository<Person>
    {
        public PersonRepository(IContext context) : base (context)
        {
        }

        public IEnumerable<Person> GetAll()
        {
            return ExecuteGetAll();
        }

        public Person GetById(int id)
        {
            return ExecuteGetById(id);
        }

        public void Insert(Person entity)
        {
            ExecuteInsert(entity);
        }

        public void Update(Person entity)
        {
            ExecuteUpdate(entity.Id, entity);
        }

        public void Delete(int id)
        {
            ExecuteDelete(id);
        }
    }
}
