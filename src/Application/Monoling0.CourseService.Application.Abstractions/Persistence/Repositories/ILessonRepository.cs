using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Models.Lessons;

namespace Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;

public interface ILessonRepository
{
    Task<Lesson> CreateAsync(Lesson lesson, CancellationToken cancellationToken);

    Task<Lesson?> FindAsync(long id, CancellationToken cancellationToken);

    Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken);

    Task DeleteAsync(long lessonId, CancellationToken cancellationToken);

    IAsyncEnumerable<Lesson> QueryAsync(LessonQuery query, CancellationToken cancellationToken);
}