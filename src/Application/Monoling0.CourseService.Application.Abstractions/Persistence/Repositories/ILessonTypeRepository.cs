namespace Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;

public interface ILessonTypeRepository
{
    Task<int> GetExperienceAsync(long lessonTypeId, CancellationToken cancellationToken);

    Task<long> GetIdAsync(string lessonTypeName, CancellationToken cancellationToken);

    Task<string> GetNameAsync(long id, CancellationToken cancellationToken);
}