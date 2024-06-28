using System.ComponentModel;

namespace ControleFinanceiro.CrossCutting.DTO
{
    public class PersonDTO : BaseDTO
    {
        public required string Name { get; set; }
        public required bool Main { get; set; }
        public required bool Inactive { get; set; }
    }
}
