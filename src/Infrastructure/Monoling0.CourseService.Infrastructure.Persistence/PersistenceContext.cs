using Monoling0.CourseService.Application.Abstractions.Persistence;
using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;

namespace Monoling0.CourseService.Infrastructure.Persistence;

internal class PersistenceContext : IPersistenceContext
{
    public PersistenceContext(
        ICourseCreatorRepository courseCreators,
        ICourseRepository courses,
        IModuleRepository modules,
        ILessonRepository lessons,
        ILessonTypeRepository lessonTypes)
    {
        CourseCreators = courseCreators;
        Courses = courses;
        Modules = modules;
        Lessons = lessons;
        LessonTypes = lessonTypes;
    }

    public ICourseCreatorRepository CourseCreators { get; }

    public ICourseRepository Courses { get; }

    public IModuleRepository Modules { get; }

    public ILessonRepository Lessons { get; }

    public ILessonTypeRepository LessonTypes { get; }
}