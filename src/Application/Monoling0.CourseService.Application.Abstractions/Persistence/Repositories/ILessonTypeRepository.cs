using Monoling0.CourseService.Application.Models.Lessons;

namespace Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;

public interface ILessonTypeRepository
{
    Task<LessonType> GetAsync(long lessonTypeId, CancellationToken cancellationToken);
}