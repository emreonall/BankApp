using BankApp.Domain.Entities;

namespace BankApp.Database.DTOs.BankDtos
{
    public class BankListDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public required string Name { get; set; }
        public string? IconUrl { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }


    }
}
