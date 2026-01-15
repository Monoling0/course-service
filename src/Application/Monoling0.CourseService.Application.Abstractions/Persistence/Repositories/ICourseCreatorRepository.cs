namespace Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;

public interface ICourseCreatorRepository
{
    Task CreateAsync(long courseId, long userId, CancellationToken cancellationToken);

    IAsyncEnumerable<long> GetCourseCreatorsAsync(long courseId, CancellationToken cancellationToken);
}