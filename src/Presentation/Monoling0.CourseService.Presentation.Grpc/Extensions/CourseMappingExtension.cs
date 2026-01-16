using Courses.CourseService.Contracts;
using Monoling0.CourseService.Application.Contracts.Courses.Operations;

namespace Monoling0.CourseService.Presentation.Grpc.Extensions;

public static class CourseMappingExtension
{
    public static CheckCourseAuthorship.Request MapToOperation(this CheckAuthorshipRequest request)
        => new CheckCourseAuthorship.Request(request.CourseId, request.UserId);

    public static CheckAuthorshipResponse MapToResponse(this CheckCourseAuthorship.Response response)
        => new CheckAuthorshipResponse
        {
            CheckResult = response.CheckResult,
        };

    public static AddCourseCreator.Request MapToOperation(this AddCourseCreatorRequest request)
        => new AddCourseCreator.Request(request.CourseId, request.UserId, request.RequestedByUserId);

    public static CreateCourse.Request MapToOperation(this CreateCourseRequest request)
        => new CreateCourse.Request(request.Name, request.Description, request.Language, request.Level.MapToModel(), request.UserId);

    public static CreateCourseResponse MapToResponse(this CreateCourse.Response response)
        => new CreateCourseResponse
        {
            Course = response.Course.MapToMessage(),
        };

    public static PublishCourse.Request MapToOperation(this PublishCourseRequest request)
        => new PublishCourse.Request(request.CourseId, request.PublishedByUserId);

    public static UnpublishCourse.Request MapToOperation(this UnpublishCourseRequest request)
        => new UnpublishCourse.Request(request.CourseId, request.Reason, request.UnpublishedByUserId);

    public static GetCourses.Request MapToOperation(this GetCourseListRequest request)
        => new GetCourses.Request(
            request.CourseIds.ToArray(),
            request.HasLanguage ? request.Language : null,
            request.HasLevel ? request.Level.MapToModel() : null,
            request.HasState ? request.State.MapToModel() : null,
            request.Cursor,
            request.PageSize);
}