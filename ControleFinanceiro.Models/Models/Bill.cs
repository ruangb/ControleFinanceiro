using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        [Column]
        public required DateTime DueDate { get; set; }
        [Column]
        [Required]
        [ForeignKey("IdCreditCard")]
        public int IdCreditCard { get; set; }
        public required decimal Value { get; set; }
        public CreditCard? CreditCard  { get; set; }
    }
}
