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
                return conn.QueryFirst<T>($"SELECT * FROM {typeof(T).Name} (NOLOCK) WHERE Id = {id}");
            }
        }

        protected int ExecuteInsert(T entity)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    List<string> fieldNames = [];

                    DataSupport<T>.SetCommandParametersForInsertByEntityValues(entity, command, fieldNames);

                    command.CommandText = DataSupport<T>.GenerateSqlInsert(fieldNames);
                    return command.ExecuteNonQuery();
                }
            }
        }

        protected int ExecuteInsert(IList<T> entity)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    List<string> fieldNames = [];

                    foreach (var item in entity)
                    {
                        DataSupport<T>.SetCommandParametersForInsertByEntityValues(item, command, fieldNames);
                    }

                    command.CommandText = DataSupport<T>.GenerateSqlInsert(fieldNames);
                    return command.ExecuteNonQuery();
                }
            }
        }

        protected void ExecuteUpdate(int id, T entity)
        {
            using (var conn = new SqlConnection(_context.GetConnectionString()))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    List<string> fieldNames = [];

                    string sql = DataSupport<T>.SetCommandParametersForUpdateByEntityValues(entity, command);

                    command.CommandText = DataSupport<T>.GenerateSqlUpdate(id, sql);
                    command.ExecuteNonQuery();
                }
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
