using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Models.Modules;

namespace Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;

public interface IModuleRepository
{
    Task<CourseModule> CreateAsync(CourseModule courseModule, CancellationToken cancellationToken);

    Task<CourseModule?> FindAsync(long id, CancellationToken cancellationToken);

    Task<CourseModule?> FindByNumberAsync(long courseId, int moduleNumber, CancellationToken cancellationToken);

    Task UpdateAsync(CourseModule courseModule, CancellationToken cancellationToken);

    Task DeleteAsync(long moduleId, CancellationToken cancellationToken);

    IAsyncEnumerable<CourseModule> QueryAsync(ModuleQuery query, CancellationToken cancellationToken);
}