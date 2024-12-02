using BankApp.Domain.Entities;

namespace BankApp.Database.ViewModels
{
    public class BankListVM
    {
        public BankListVM()
        {
            CompnayBalances = new List<CompnayBalance>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? IconUrl { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<CompnayBalance>? CompnayBalances { get; set; }
    }
}
