using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Monoling0.CourseService.Infrastructure.Persistence.Migrations;

namespace Monoling0.CourseService.Infrastructure.Persistence.Extenstions;

public static class MigrationExtensions
{
    public static IServiceCollection AddMigrations(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString("Host=localhost;Port=7654;Database=postgres;Username=postgres;Password=postgres") // provider => provider.GetRequiredService<IOptions<PostgresOptions>>().Value.ConnectionString
                .WithMigrationsIn(typeof(Initial).Assembly));

        return serviceCollection;
    }

    public static async Task RunMigrations(this IServiceProvider serviceProvider)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();
        IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}