using Monoling0.CourseService.Application.Contracts.Lessons.Models;
using Monoling0.CourseService.Application.Models.Lessons.LessonContents;

namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class CreateLesson
{
    public readonly record struct Request(long ModuleId, long LessonTypeId, string Name, string Description, LessonContent Content);

    public readonly record struct Response(LessonDto Lesson);
}