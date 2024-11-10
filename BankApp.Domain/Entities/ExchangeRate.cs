namespace BankApp.Domain.Entities
{
    public class ExchangeRate:BaseEntity
    {
        public required DateOnly CurrentDate { get; set; }
        public decimal Rate { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency  { get; set; }
    }
}
