using Courses.CourseService.Contracts;
using Grpc.Core;
using Monoling0.CourseService.Application.Contracts.Courses;
using Monoling0.CourseService.Application.Contracts.Courses.Operations;
using Monoling0.CourseService.Presentation.Grpc.Extensions;
using CoursesService = Courses.CourseService.Contracts.CourseService;

namespace Monoling0.CourseService.Presentation.Grpc.Controllers;

public class CourseController : CoursesService.CourseServiceBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public override async Task<CheckAuthorshipResponse> CheckAuthorship(CheckAuthorshipRequest request, ServerCallContext context)
    {
        CheckCourseAuthorship.Response response = await _courseService.CheckCourseAuthorship(request.MapToOperation(), context.CancellationToken);
        return response.MapToResponse();
    }

    public override async Task<AddCourseCreatorResponse> AddCourseCreator(AddCourseCreatorRequest request, ServerCallContext context)
    {
        await _courseService.AddCourseCreator(request.MapToOperation(), context.CancellationToken);
        return new AddCourseCreatorResponse();
    }

    public override async Task<CreateCourseResponse> CreateCourse(CreateCourseRequest request, ServerCallContext context)
    {
        CreateCourse.Response response = await _courseService.CreateAsync(request.MapToOperation(), context.CancellationToken);
        return response.MapToResponse();
    }

    public override Task<UpdateCourseResponse> UpdateCourse(UpdateCourseRequest request, ServerCallContext context)
    {
        return base.UpdateCourse(request, context);
    }

    public override async Task<PublishCourseResponse> PublishCourse(PublishCourseRequest request, ServerCallContext context)
    {
        PublishCourse.Result response = await _courseService.PublishAsync(request.MapToOperation(), context.CancellationToken);
        return response switch
        {
            Application.Contracts.Courses.Operations.PublishCourse.Result.Success => new PublishCourseResponse(),

            PublishCourse.Result.CourseNotFound _ => throw new RpcException(
                new Status(StatusCode.NotFound, $"Course with id {request.CourseId} not found")),

            Application.Contracts.Courses.Operations.PublishCourse.Result.AlreadyPublished => throw new RpcException(
                new Status(StatusCode.InvalidArgument, $"Course {request.CourseId} already published")),

            _ => throw new Exception("Unknown result"),
        };
    }

    public override async Task<UnpublishCourseResponse> UnpublishCourse(UnpublishCourseRequest request, ServerCallContext context)
    {
        UnpublishCourse.Result response = await _courseService.UnpublishAsync(request.MapToOperation(), context.CancellationToken);
        return response switch
        {
            Application.Contracts.Courses.Operations.UnpublishCourse.Result.Success => new UnpublishCourseResponse(),

            UnpublishCourse.Result.CourseNotFound _ => throw new RpcException(
                new Status(StatusCode.NotFound, $"Course with id {request.CourseId} not found")),

            Application.Contracts.Courses.Operations.UnpublishCourse.Result.CannotUnpublish => throw new RpcException(
                new Status(StatusCode.InvalidArgument, $"You cannot unpublish course {request.CourseId}")),

            _ => throw new Exception("Unknown result"),
        };
    }

    public override async Task<GetCourseListResponse> GetCourseList(GetCourseListRequest request, ServerCallContext context)
    {
        GetCourses.Response response = await _courseService.GetCourseListAsync(request.MapToOperation(), context.CancellationToken);
        return new GetCourseListResponse
        {
            Courses = { response.Courses.Select(c => c.MapToMessage()) },
        };
    }
}