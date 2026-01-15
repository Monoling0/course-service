using Monoling0.CourseService.Application.Models.Lessons.LessonContents;

namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class UpdateLesson
{
    public readonly record struct Request(long LessonId, string? Name, string? Description, LessonContent? Content);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success() : Response;

        public sealed record LessonNotFound : Response;
    }
}