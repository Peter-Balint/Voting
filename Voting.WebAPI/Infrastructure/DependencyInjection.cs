using AutoMapper;

namespace Voting.WebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAutoMapperCustom(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new Mapping()));
            mapperConfig.AssertConfigurationIsValid();

            services.AddAutoMapper(typeof(Mapping));

            return services;
        }
    }
}
