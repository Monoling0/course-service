using Monoling0.CourseService.Application.Contracts.Courses.Operations;

namespace Monoling0.CourseService.Application.Contracts.Courses;

public interface ICourseService
{
    Task<CreateCourse.Response> CreateAsync(CreateCourse.Request request, CancellationToken cancellationToken);

    Task<UpdateCourse.Result> UpdateAsync(UpdateCourse.Request request, CancellationToken cancellationToken);

    Task DeleteAsync(DeleteCourse.Request request, CancellationToken cancellationToken);

    Task<PublishCourse.Result> PublishAsync(PublishCourse.Request request, CancellationToken cancellationToken);

    Task<UnpublishCourse.Result> UnpublishAsync(UnpublishCourse.Request request, CancellationToken cancellationToken);

    Task<GetCourses.Response> GetCourseListAsync(GetCourses.Request request, CancellationToken cancellationToken);

    Task<CheckCourseAuthorship.Response> CheckCourseAuthorship(CheckCourseAuthorship.Request request, CancellationToken cancellationToken);

    Task AddCourseCreator(AddCourseCreator.Request request, CancellationToken cancellationToken);
}