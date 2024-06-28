using System.ComponentModel;

namespace ControleFinanceiro.CrossCutting.DTO
{
    public class CreditCardDTO : BaseDTO
    {
        public required string Name { get; set; }
        public required short DueDay { get; set; }
        public required short ClosingDays { get; set; }
        public required bool Inactive { get; set; }
    }
}
