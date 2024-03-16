using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Domain.Manager.Implementation
{
    public sealed class PersonManager : IBaseManager<Person>
    {
        private readonly IBaseRepository<Person> _baseRepository;

        public PersonManager(IBaseRepository<Person> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public IEnumerable<Person> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public Person GetById(int id)
        {
            return _baseRepository.GetById(id);
        }

        public void Insert(Person entity)
        {
            _baseRepository.Insert(entity);
        }

        public void Update(Person entity)
        {
            _baseRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _baseRepository.Delete(id);
        }

    }
}
