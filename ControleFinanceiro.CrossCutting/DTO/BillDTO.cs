namespace ControleFinanceiro.CrossCutting.DTO
{
    public class BillDTO : BaseDTO
    {
        public required DateTime DueDate { get; set; }
        public int IdCreditCard { get; set; }
        public required decimal Value { get; set; }
        public CreditCardDTO? CreditCard { get; set; }
    }
}
