using AutoMapper;
using BankApp.Database.DTOs.BankDtos;


namespace BankApp.Database.MapProfiles.Bank
{
    public class BankMapProfile : Profile
    {
        public BankMapProfile()
        {
           // CreateMap<BankCreateDto, BankApp.Domain.Entities.Bank>()
           //.ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Otomatik olarak set edilebilir veya ihtiyaca göre kontrol edilebilir
           //.ForMember(dest => dest.Id, opt => opt.Ignore()) // ID genellikle DB tarafından belirlenir
           // .ForMember(dest => dest.IsActive , opt => opt.Ignore()) // ID genellikle DB tarafından belirlenir
           // .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore()) // ID genellikle DB tarafından belirlenir
           // .ForMember(dest => dest.Transactions, opt => opt.Ignore()); // ID genellikle DB tarafından belirlenir

           // // Bank'tan DTO'ya geri dönüşüm gerekiyorsa (isteğe bağlı)
           // CreateMap<BankApp.Domain.Entities.Bank, BankCreateDto>();

            CreateMap<BankApp.Domain.Entities.Bank, BankCreateDto>();
            CreateMap<BankCreateDto, BankApp.Domain.Entities.Bank>();
            CreateMap<BankApp.Domain.Entities.Bank, BankListDto>().ReverseMap();
        }
    }
}
