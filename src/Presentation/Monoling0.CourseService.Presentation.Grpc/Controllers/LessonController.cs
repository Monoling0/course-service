using Grpc.Core;
using Lessons.CourseService.Contracts;

namespace Monoling0.CourseService.Presentation.Grpc.Controllers;

public class LessonController : LessonService.LessonServiceBase
{
    public override Task<AddLessonResponse> AddLesson(AddLessonRequest request, ServerCallContext context)
    {
        return base.AddLesson(request, context);
    }

    public override Task<UpdateLessonResponse> UpdateLesson(UpdateLessonRequest request, ServerCallContext context)
    {
        return base.UpdateLesson(request, context);
    }

    public override Task<DeleteLessonResponse> DeleteLesson(DeleteLessonRequest request, ServerCallContext context)
    {
        return base.DeleteLesson(request, context);
    }

    public override Task<SwapLessonsResponse> SwapLessons(SwapLessonsRequest request, ServerCallContext context)
    {
        return base.SwapLessons(request, context);
    }

    public override Task<GetLessonListResponse> GetLessonList(GetLessonListRequest request, ServerCallContext context)
    {
        return base.GetLessonList(request, context);
    }
}