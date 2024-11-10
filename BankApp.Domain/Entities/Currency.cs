namespace BankApp.Domain.Entities
{
    public  class Currency:BaseEntity
    {
        public required string Name { get; set; }
        public required string ShortName { get; set; }
        public string FullName => string.Join("-", ShortName, Name);
        public string? IconUrl { get; set; }
        public ICollection<ExchangeRate>? ExchangeRates { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
