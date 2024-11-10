namespace BankApp.Domain.Entities
{
    public class Bank : BaseEntity
    {
   
        public required string Name { get; set; }
        public string? IconUrl { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
