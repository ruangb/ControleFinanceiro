using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("CreditCard")]
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }
        [Column]
        public required string Name { get; set; }
        [Column]
        public required short DueDay { get; set; }
        [Column]
        public required short ClosingDays { get; set; }
        [Column]
        public required bool Inactive { get; set; }
    }
}
