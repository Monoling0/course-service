using Monoling0.CourseService.Application.Contracts.Lessons.Models;

namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class GetLesson
{
    public readonly record struct Request(long ModuleId, long[] LessonId, long[] LessonTypeId, long Cursor, long PageSize);

    public readonly record struct Response(LessonDto Lesson);
}