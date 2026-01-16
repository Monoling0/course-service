using Microsoft.Extensions.DependencyInjection;
using Monoling0.CourseService.Application.Contracts.Courses;
using Monoling0.CourseService.Application.Contracts.Lessons;
using Monoling0.CourseService.Application.Contracts.Modules;

namespace Monoling0.CourseService.Application.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICourseService, CoursesService>();
        serviceCollection.AddScoped<IModuleService, ModuleService>();
        serviceCollection.AddScoped<ILessonService, LessonService>();

        return serviceCollection;
    }
}