using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Models.Courses;

namespace Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;

public interface ICourseRepository
{
    Task<Course> CreateAsync(Course course, CancellationToken cancellationToken);

    Task<Course?> FindAsync(long id, CancellationToken cancellationToken);

    Task UpdateAsync(Course course, CancellationToken cancellationToken);

    Task DeleteAsync(long courseId, CancellationToken cancellationToken);

    IAsyncEnumerable<Course> QueryAsync(CourseQuery query, CancellationToken cancellationToken);
}