using AutoMapper;
using BankApp.Database.ViewModels;


namespace BankApp.Database.MapProfiles.Bank
{
    public class BankMapProfile : Profile
    {
        public BankMapProfile()
        {
            CreateMap<BankApp.Domain.Entities.Bank, BankListVM>().ForMember(des=>des.Id,opt=>opt.MapFrom(src=>src.Id));
        }
    }
}
