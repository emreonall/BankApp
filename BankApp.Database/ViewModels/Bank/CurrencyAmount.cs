namespace BankApp.Database.ViewModels.Bank
{
    public class CurrencyAmount
    {
        public string? CurrencyName { get; set; } // Currency tablosundan gelen Name alanı
        public decimal TotalAmount { get; set; }
    }
}