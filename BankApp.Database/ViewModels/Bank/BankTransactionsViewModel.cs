namespace BankApp.Database.ViewModels.Bank
{
    public class BankTransactionsViewModel
    {
        public int BankId { get; set; }
        public string? Name { get; set; }
        public string? IconURL { get; set; }
        public List<CurrencyAmount> CurrencyTotals { get; set; } = new();
    }
}
