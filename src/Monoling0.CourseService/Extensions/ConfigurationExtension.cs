using Monoling0.CourseService.Infrastructure.Persistence;

namespace Monoling0.CourseService.Extensions;

public static class ConfigurationExtension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .AddOptions()
            .Configure<PostgresOptions>(configuration.GetSection("Infrastructure:Persistence:Postgres"));

        return serviceCollection;
    }
}