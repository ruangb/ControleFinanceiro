using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class ExpenseRepository : BaseRepository<Expense>, IBaseRepository<Expense>
    {
        public ExpenseRepository(IContext context) : base (context)
        {
        }

        public IEnumerable<Expense> GetAll()
        {
            return ExecuteGetAll();
        }

        public Expense GetById(int id)
        {
            return ExecuteGetById(id);
        }

        public int Insert(Expense entity)
        {
            return ExecuteInsert(entity);
        }

        public void Update(Expense entity)
        {
            ExecuteUpdate(entity.Id, entity);
        }

        public void Delete(int id)
        {
            ExecuteDelete(id);
        }
    }
}
