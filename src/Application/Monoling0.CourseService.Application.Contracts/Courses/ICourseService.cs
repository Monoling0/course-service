using Monoling0.CourseService.Application.Contracts.Courses.Operations;

namespace Monoling0.CourseService.Application.Contracts.Courses;

public interface ICourseService
{
    Task<CreateCourse.Response> CreateAsync(CreateCourse.Request request, CancellationToken cancellationToken);

    Task UpdateAsync(UpdateCourse.Request request, CancellationToken cancellationToken);

    Task DeleteAsync(DeleteCourse.Request request, CancellationToken cancellationToken);

    Task PublishAsync(PublishCourse.Request request, CancellationToken cancellationToken);

    Task UnpublishAsync(UnpublishCourse.Request request, CancellationToken cancellationToken);

    IAsyncEnumerable<GetCourses.Response> GetCourseListAsync(GetCourses.Request request, CancellationToken cancellationToken);

    Task<CheckCourseAuthorship.Response> CheckCourseAuthorship(CheckCourseAuthorship.Request request, CancellationToken cancellationToken);

    Task AddCourseCreator(AddCourseCreator.Request request, CancellationToken cancellationToken);
}