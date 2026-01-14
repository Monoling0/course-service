using Courses.CourseService.Contracts;
using Grpc.Core;
using CoursesService = Courses.CourseService.Contracts.CourseService;

namespace Monoling0.CourseService.Presentation.Grpc.Controllers;

public class CourseController : CoursesService.CourseServiceBase
{
    public override Task<CheckAuthorshipResponse> CheckAuthorship(CheckAuthorshipRequest request, ServerCallContext context)
    {
        return base.CheckAuthorship(request, context);
    }

    public override Task<AddCourseCreatorResponse> AddCourseCreator(AddCourseCreatorRequest request, ServerCallContext context)
    {
        return base.AddCourseCreator(request, context);
    }

    public override Task<CreateCourseResponse> CreateCourse(CreateCourseRequest request, ServerCallContext context)
    {
        return base.CreateCourse(request, context);
    }

    public override Task<UpdateCourseResponse> UpdateCourse(UpdateCourseRequest request, ServerCallContext context)
    {
        return base.UpdateCourse(request, context);
    }

    public override Task<DeleteCourseResponse> DeleteCourse(DeleteCourseRequest request, ServerCallContext context)
    {
        return base.DeleteCourse(request, context);
    }

    public override Task<PublishCourseResponse> PublishCourse(PublishCourseRequest request, ServerCallContext context)
    {
        return base.PublishCourse(request, context);
    }

    public override Task<UnpublishCourseResponse> UnpublishCourse(UnpublishCourseRequest request, ServerCallContext context)
    {
        return base.UnpublishCourse(request, context);
    }

    public override Task<GetCourseListResponse> GetCourseList(GetCourseListRequest request, ServerCallContext context)
    {
        return base.GetCourseList(request, context);
    }
}