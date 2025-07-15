using AutoMapper;
using System.Runtime.CompilerServices;

namespace QuaveChallenge.API.Services.Mapper
{
    public static class AddAutoMapperConfiguration
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
