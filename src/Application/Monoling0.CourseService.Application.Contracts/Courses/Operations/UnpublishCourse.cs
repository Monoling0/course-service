namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class UnpublishCourse
{
    public readonly record struct Request(long CourseId, string? Reason, long UnpublishedByUserId);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record CourseNotFound : Result;

        public sealed record CannotUnpublish : Result;
    }
}