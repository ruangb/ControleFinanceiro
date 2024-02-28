using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("CreditCard")]
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required short DueDay { get; set; }
        public required short ClosingDays { get; set; }
        public required bool Inactive { get; set; }
    }
}
