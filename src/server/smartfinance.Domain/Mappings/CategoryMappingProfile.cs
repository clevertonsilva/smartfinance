using AutoMapper;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Models.AccountMovementCategory.Create;

namespace smartfinance.Domain.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryCreateViewModel, Category>();
        }
    }
}
