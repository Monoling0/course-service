using Monoling0.CourseService.Application.Models.Courses;

namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class UpdateCourse
{
    public readonly record struct Request(long CourseId, string? Name, string? Description, CefrLevel? Level);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record CourseNotFound : Result;
    }
}