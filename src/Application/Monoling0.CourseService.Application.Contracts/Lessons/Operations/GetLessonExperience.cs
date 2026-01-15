namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class GetLessonExperience
{
    public readonly record struct Request(long LessonId);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(int Experience) : Response;

        public sealed record LessonNotFound : Response;
    }
}