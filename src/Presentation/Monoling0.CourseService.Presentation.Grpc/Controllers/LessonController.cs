using Grpc.Core;
using Lessons.CourseService.Contracts;
using Monoling0.CourseService.Application.Contracts.Lessons;
using Monoling0.CourseService.Application.Contracts.Lessons.Operations;
using Monoling0.CourseService.Presentation.Grpc.Extensions;

namespace Monoling0.CourseService.Presentation.Grpc.Controllers;

public class LessonController : LessonService.LessonServiceBase
{
    private readonly ILessonService _lessonService;

    public LessonController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public override async Task<AddLessonResponse> AddLesson(AddLessonRequest request, ServerCallContext context)
    {
        CreateLesson.Response response = await _lessonService.AddLessonAsync(request.MapToOperation(), context.CancellationToken);

        return response switch
        {
            CreateLesson.Response.ModuleNotFound => throw new RpcException(
                new Status(StatusCode.NotFound, $"Module with id {request.ModuleId} not found")),

            CreateLesson.Response.Success success => new AddLessonResponse()
                { Lesson = success.Lesson.MapToMessage() },
            _ => throw new Exception("Unknown result"),
        };
    }

    public override Task<UpdateLessonResponse> UpdateLesson(UpdateLessonRequest request, ServerCallContext context)
    {
        return base.UpdateLesson(request, context);
    }

    public override async Task<GetLessonListResponse> GetLessonList(GetLessonListRequest request, ServerCallContext context)
    {
         GetLessons.Response response = await _lessonService.GetLessonListAsync(request.MapToOperation(), context.CancellationToken);

         return response switch
         {
             GetLessons.Response.ModuleNotFound => throw new RpcException(
                 new Status(StatusCode.NotFound, $"Module with id {request.ModuleIds} not found")),

             GetLessons.Response.Success success => new GetLessonListResponse()
                 { Lessons = { success.Lesson.Select(l => l.MapToMessage()) } },
             _ => throw new Exception("Unknown result"),
         };
    }
}