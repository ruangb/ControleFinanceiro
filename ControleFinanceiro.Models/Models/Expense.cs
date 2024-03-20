using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    [Table("Expense")]
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public required string Status { get; set; }
        public required DateTime OperationDate { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        public required short ParcelQuantity { get; set; }
        public int IdPerson { get; set; }
        public int IdExpense { get; set; }
        public required Person Person { get; set; }
        public CreditCard? CreditCard { get; set; }
    }
}
