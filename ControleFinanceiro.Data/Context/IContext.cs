using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data.Context
{
    internal interface IContext
    {
        string? GetConnectionString();
    }
}
