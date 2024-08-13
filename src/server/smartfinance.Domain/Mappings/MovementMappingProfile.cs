using AutoMapper;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Models.AccountMovement.Create;
using smartfinance.Domain.Models.AccountMovement.Model;

namespace smartfinance.Domain.Mappings
{
    public class MovementMappingProfile : Profile
    {
        public MovementMappingProfile() 
        {
            CreateMap<MovementCreateViewModel, Movement>();
            CreateMap<IEnumerable<Movement>, IEnumerable<MovementViewModel>>();
        }
    }
}
