using Monoling0.CourseService.Application.Contracts.Lessons.Models;
using Monoling0.CourseService.Application.Models.Lessons.LessonContents;

namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class UpdateLesson
{
    public readonly record struct Request(string Name, string Description, LessonContent Content);

    public readonly record struct Response(LessonDto Lesson);
}