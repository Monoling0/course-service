using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;

namespace Monoling0.CourseService.Application.Abstractions.Persistence;

public interface IPersistenceContext
{
    ICourseCreatorRepository CourseCreators { get; }

    ICourseRepository Courses { get; }

    IModuleRepository Modules { get; }

    ILessonRepository Lessons { get; }

    ILessonTypeRepository LessonTypes { get; }
}