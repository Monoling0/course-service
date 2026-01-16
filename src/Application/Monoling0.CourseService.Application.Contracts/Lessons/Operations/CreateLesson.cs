using Monoling0.CourseService.Application.Contracts.Lessons.Models;
using Monoling0.CourseService.Application.Models.Lessons;
using Monoling0.CourseService.Application.Models.Lessons.LessonContents;

namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class CreateLesson
{
    public readonly record struct Request(long ModuleId, LessonKind Kind, string Name, string Description, LessonContent Content);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(LessonDto Lesson) : Response;

        public sealed record ModuleNotFound : Response;
    }
}