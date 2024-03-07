using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using Dapper;
using System.Data.SqlClient;

namespace ControleFinanceiro.Data.Implementation
{
    public sealed class CreditCardRepository : BaseRepository<CreditCard>, IBaseRepository<CreditCard>
    {
        private readonly IContext _context;

        public CreditCardRepository(IContext context)
        {
            _context = context;
        }

        public IEnumerable<CreditCard> GetAll()
        {
            using (var db = new SqlConnection(_context.GetConnectionString()))
            {
                db.Open();

                var creditCards = db.Query<CreditCard>("SELECT * FROM CreditCard (NOLOCK)");

                return creditCards;
            }
        }

        public CreditCard GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(CreditCard entity)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    ExecuteInsert(entity, command);
                }
            }
        }

        public void Update(CreditCard entity)
        {
            throw new NotImplementedException();
        }
    }
}
