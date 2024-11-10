namespace BankApp.Domain.Entities
{
    public class Transaction:BaseEntity
    {
        private decimal amountInPublicCurrency;

        public int CompanyId { get; set; }
        public int BankId { get; set; }
        public int CurrencyId { get; set; }
        public int TransactionTypeId { get; set; }

        public DateOnly CurrentDate { get; set; }
        public decimal Amount { get; set; } = 0;
        public decimal ExchRate { get; set; } = 0;
        public decimal AmountInPublicCurrency { get => amountInPublicCurrency; set => amountInPublicCurrency = Amount*ExchRate; }
        public string? Description { get; set; }

        public Company? Company { get; set; }
        public Bank? Bank { get; set; }
        public Currency? Currency { get; set; }
        public TransactionType? TransactionType { get; set; }
    }
}
