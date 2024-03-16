using System.ComponentModel;

namespace ControleFinanceiro.CrossCutting.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required bool Main { get; set; }
        public required bool Inactive { get; set; }
    }
}
