using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("ExpenseInstallment")]
    public class ExpenseInstallment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Column]
        [Required]
        [ForeignKey("IdExpense")]
        public int IdExpense { get; set; }

        [Column]
        [Required]
        [ForeignKey("IdBill")]
        public int IdBill { get; set; }

        [Column]
        public short Installment { get; set; }

        [Column]
        public string Status { get; set; }

        [Column]
        public DateTime ReferenceDate { get; set; }

        [Column]
        public decimal Value { get; set; }

        public Expense? Expense { get; set; }

        public ExpenseInstallment(int id, int idExpense, short installment, string status, DateTime referenceDate, decimal value)
        {
            Id = id;
            IdExpense = idExpense;
            Installment = installment;
            Status = status;
            ReferenceDate = referenceDate;
            Value = value;
        }

        public ExpenseInstallment()
        {
        }
    }
}
