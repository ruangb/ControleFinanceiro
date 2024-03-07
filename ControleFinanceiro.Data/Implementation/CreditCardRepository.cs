using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Models;
using Dapper;
using System.Data.SqlClient;
using ControleFinanceiro.CrossCutting.Utilities;

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
            using (var db = new SqlConnection(_context.GetConnectionString()))
            {
                db.Open();

                Dictionary<string, object> entityKeyValues = GetEntityKeyValues(entity);

                string sql = $"INSERT INTO CreditCard (NOLOCK) ({entityKeyValues.Keys.AsList().Parameterize()})";
                                            //+ $"VALUES ({values})";

                var creditCards = db.Execute(sql);
            }
        }


        public void Update(CreditCard entity)
        {
            throw new NotImplementedException();
        }
    }
}
