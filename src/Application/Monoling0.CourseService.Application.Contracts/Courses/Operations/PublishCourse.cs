namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class PublishCourse
{
    public readonly record struct Request(long CourseId, long PublishedByUserId);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success : Result;

        public sealed record CourseNotFound() : Result;

        public sealed record AlreadyPublished : Result;
    }
}