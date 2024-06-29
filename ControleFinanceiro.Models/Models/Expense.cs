using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("Expense")]
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Column]
        public string Status { get; set; }
        [Column]
        [ReadOnly(true)]
        public DateTime OperationDate { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public decimal Amount { get; set; }
        [Column]
        [ReadOnly(true)]
        public short ParcelQuantity { get; set; }
        [Column]
        [Required]
        [ForeignKey("IdPerson")]
        public int IdPerson { get; set; }
        [Column]
        [ForeignKey("IdCreditCard")]
        [ReadOnly(true)]
        public int? IdCreditCard { get; set; }
        public Person Person { get; set; }
        public CreditCard? CreditCard { get; set; }
        public List<ExpenseInstallment>? ExpenseInstallments { get; set; }
    }
}
