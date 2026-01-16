using Lessons.CourseService.Contracts;
using Monoling0.CourseService.Application.Contracts.Lessons.Operations;

namespace Monoling0.CourseService.Presentation.Grpc.Extensions;

public static class LessonMappingExtension
{
    public static CreateLesson.Request MapToOperation(this AddLessonRequest request)
        => new CreateLesson.Request(
            request.ModuleId,
            request.LessonType.MapToModel(),
            request.Name,
            request.Description,
            request.MapContent());

    public static GetLessons.Request MapToOperation(this GetLessonListRequest request)
        => new GetLessons.Request(
            request.ModuleIds,
            request.LessonIds.ToArray(),
            request.LessonTypes.Select(t => t.MapToModel()).ToArray(),
            request.Cursor,
            request.PageSize);
}