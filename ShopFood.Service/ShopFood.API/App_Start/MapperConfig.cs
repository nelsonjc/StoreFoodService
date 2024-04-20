using AutoMapper;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Entities;

namespace ShopFood.API.App_Start
{
    public static class MapperConfig
    {
        /// <summary>
        /// Add Auto Mapper Configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            return services.AddSingleton(mapper);
        }

        /// <summary>
        /// Mapping Profile configuration
        /// </summary>
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<User, UserDto>();
                CreateMap<UserRequest, User>();
                CreateMap<User, UserAuthenticationResultDto>().ForMember(dest => dest.User, dto => dto.MapFrom(x => x));

                CreateMap<FoodCatalog, FoodCatalogDto>();
                CreateMap<FoodCatalogRequest, FoodCatalog>();
                CreateMap<FoodCatalog, FoodCatalogCustomerDto>();
            }
        }
    }
}
