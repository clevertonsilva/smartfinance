using AutoMapper;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Models.Account.Create;
using smartfinance.Domain.Models.Authentication.Model;

namespace smartfinance.Domain.Mappings
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<AccountCreateViewModel, Account>()
                .ForMember(f => f.Movements, opt => opt.Ignore());

            CreateMap<AccountCreateViewModel, IdentityUserViewModel>();
            CreateMap<AccountCreateViewModel, AppIdentityUserViewModel>()
                .ForMember(f => f.Id, opt => opt.Ignore());

        }
    }
}
