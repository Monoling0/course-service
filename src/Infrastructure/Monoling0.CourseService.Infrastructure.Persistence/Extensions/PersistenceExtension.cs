using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Monoling0.CourseService.Application.Abstractions.Persistence;
using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;
using Monoling0.CourseService.Application.Models.Courses;
using Monoling0.CourseService.Infrastructure.Persistence.Repositories;
using Npgsql;

namespace Monoling0.CourseService.Infrastructure.Persistence.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPersistenceContext, PersistenceContext>();

        serviceCollection.AddScoped<ICourseCreatorRepository, CourseCreatorsRepository>();
        serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
        serviceCollection.AddScoped<IModuleRepository, ModuleRepository>();
        serviceCollection.AddScoped<ILessonRepository, LessonRepository>();
        serviceCollection.AddScoped<ILessonTypeRepository, LessonTypeRepository>();

        return serviceCollection;
    }

    public static IServiceCollection ConfigureDatasource(this IServiceCollection serviceCollection)
    {
        IOptions<PostgresOptions> postgresOptions =
            serviceCollection.BuildServiceProvider().GetRequiredService<IOptions<PostgresOptions>>();
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresOptions.Value.ConnectionString);
        dataSourceBuilder.MapEnum<CefrLevel>(pgName: "cefr_level");
        dataSourceBuilder.MapEnum<CourseState>(pgName: "course_state");
        serviceCollection.AddSingleton(dataSourceBuilder.Build());

        return serviceCollection;
    }
}