using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.CrossCutting.DTO
{
    public class ExpenseDTO : BaseDTO
    {
        public string Status { get; set; }
        public DateTime OperationDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public short ParcelQuantity { get; set; }
        public int IdPerson { get; set; }
        public int? IdCreditCard { get; set; }
        public PersonDTO Person { get; set; }
        public CreditCardDTO? CreditCard { get; set; }
        public List<ExpenseInstallmentDTO>? ExpenseInstallments { get; set; }
        [NotMapped]
        public bool OnlyThirds { get; set; }

    }
}
