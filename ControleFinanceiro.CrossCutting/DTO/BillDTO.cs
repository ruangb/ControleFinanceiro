using System.ComponentModel;

namespace ControleFinanceiro.CrossCutting.DTO
{
    public class BillDTO : BaseDTO
    {
        public DateTime DueDate { get; set; }
        public int IdCreditCard { get; set; }
        public decimal Value { get; set; }
        public CreditCardDTO? CreditCard { get; set; }
        public int IdPerson { get; set; }
        public DateTime StartDueDate { get; set; }
        public bool OnlyThirds { get; set; }

        public BillDTO()
        {
        }

        public BillDTO(DateTime startDueDate)
        {
            StartDueDate = startDueDate;
        }
    }
}
