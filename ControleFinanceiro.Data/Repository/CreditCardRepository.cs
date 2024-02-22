using ControleFinanceiro.Data.Context;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data.Repository
{
    internal class CreditCardRepository : IGenericRepository<CreditCard>
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

        public void Insert(CreditCard obj)
        {
            throw new NotImplementedException();
        }

        public void Update(CreditCard obj)
        {
            throw new NotImplementedException();
        }
    }
}
