using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data.Context
{
    public interface IContext
    {
        string? GetConnectionString();
    }
}
