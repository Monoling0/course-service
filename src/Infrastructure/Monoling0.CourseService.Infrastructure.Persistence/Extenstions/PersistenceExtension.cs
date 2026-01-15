using Microsoft.Extensions.DependencyInjection;
using Monoling0.CourseService.Application.Abstractions.Persistence;
using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;
using Monoling0.CourseService.Application.Models.Courses;
using Monoling0.CourseService.Infrastructure.Persistence.Repositories;
using Npgsql;

namespace Monoling0.CourseService.Infrastructure.Persistence.Extenstions;

public static class PersistenceExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPersistenceContext, PersistenceContext>();

        serviceCollection.AddScoped<ICourseCreatorRepository, ICourseCreatorRepository>();
        serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
        serviceCollection.AddScoped<IModuleRepository, ModuleRepository>();
        serviceCollection.AddScoped<ILessonRepository, LessonRepository>();
        serviceCollection.AddScoped<ILessonTypeRepository, LessonTypeRepository>();

        return serviceCollection;
    }

    public static IServiceCollection ConfigureDatasource(this IServiceCollection serviceCollection)
    {
        // get from options !!!
        var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=localhost;Port=7654;Database=postgres;Username=postgres;Password=postgres");
        dataSourceBuilder.MapEnum<CefrLevel>(pgName: "cefr_level");
        dataSourceBuilder.MapEnum<CourseState>(pgName: "course_state");
        serviceCollection.AddSingleton(dataSourceBuilder.Build());

        return serviceCollection;
    }
}