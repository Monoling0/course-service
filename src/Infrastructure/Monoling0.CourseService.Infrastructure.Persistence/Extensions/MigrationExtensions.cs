using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Monoling0.CourseService.Infrastructure.Persistence.Migrations;

namespace Monoling0.CourseService.Infrastructure.Persistence.Extensions;

public static class MigrationExtensions
{
    public static IServiceCollection AddMigrations(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString(provider => provider.GetRequiredService<IOptions<PostgresOptions>>().Value.ConnectionString)
                .WithMigrationsIn(typeof(Initial).Assembly));

        serviceCollection.AddHostedService<MigrationRunnerService>();

        return serviceCollection;
    }

    // public static async Task RunMigrations(this IServiceProvider serviceProvider)
    // {
    //     await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();
    //     IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    //     runner.MigrateUp();
    // }
}