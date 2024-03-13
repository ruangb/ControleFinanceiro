using ControleFinanceiro.CrossCutting.Utilities;
using ControleFinanceiro.Data.Context;
using Dapper;
using System.Data.SqlClient;

namespace ControleFinanceiro.Data.Implementation
{
    public class BaseRepository<T> where T : class
    {
        private readonly IContext _context;

        public BaseRepository(IContext context)
        {
            _context = context;
        }

        protected IEnumerable<T> ExecuteGetAll()
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();
                return conn.Query<T>($"SELECT * FROM {typeof(T).Name} (NOLOCK)");
            }
        }

        protected T ExecuteGetById(int id)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();
                return conn.QueryFirst<T>($"SELECT * FROM {typeof(T).Name} WHERE Id = {id}");
            }
        }

        protected void ExecuteInsert(T entity)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    List<string> fieldNames = [];

                    DataSupport<T>.SetCommandParametersByEntityValues(entity, command, fieldNames);

                    command.CommandText = DataSupport<T>.GenerateSqlInsert(fieldNames);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void ExecuteUpdate(int id, T entity)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();
                conn.Execute(DataSupport<T>.GenerateSqlUpdate(id, entity));
            }
        }

        protected void ExecuteDelete(int id)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();
                conn.Execute($"DELETE FROM {typeof(T).Name} WHERE Id = {id}");    
            }
        }
    }
}
