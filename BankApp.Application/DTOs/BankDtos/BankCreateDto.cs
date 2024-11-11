using System.ComponentModel.DataAnnotations;

namespace BankApp.Application.DTOs.BankDtos
{
    public class BankCreateDto
    {
        [Required(ErrorMessage ="{Name} alanı boş geçilemez.")]
        [MaxLength(20, ErrorMessage = "{Name} alanı en fazla {1} karakter olmalıdır.")]
        [Display(Name = "Banka Adı")]
        public required string Name { get; set; }
        [MaxLength(255, ErrorMessage = "{IconUrl} alanı en fazla {1} karakter olmalıdır.")]
        [Display(Name = "Logo Dosyası")]
        public string? IconUrl { get; set; }
    }
}
