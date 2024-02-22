using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data.Context
{
    internal class Context : IContext
    {
        public IConfiguration Configuration { get; }
        public Context(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string? GetConnectionString()
        {
            return Configuration.GetConnectionString("CFConnection");
        }
    }
}
