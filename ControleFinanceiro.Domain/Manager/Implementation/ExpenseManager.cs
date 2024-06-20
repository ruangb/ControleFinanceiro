using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using AutoMapper;
using ControleFinanceiro.CrossCutting.DTO;

namespace ControleFinanceiro.Domain.Manager.Implementation
{
    public sealed class ExpenseManager : IBaseManager<Expense>
    {
        private readonly IBaseRepository<Expense> _baseRepository;
        private readonly IMapper _mapper;

        public ExpenseManager(IBaseRepository<Expense> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public IEnumerable<Expense> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public Expense GetById(int id)
        {
            return _baseRepository.GetById(id);
        }

        public int Insert(Expense entity)
        {
            return _baseRepository.Insert(entity);
        }

        public void Update(Expense entity)
        {
            _baseRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _baseRepository.Delete(id);
        }

    }
}
