using Monoling0.CourseService.Application.Contracts.Lessons.Models;
using Monoling0.CourseService.Application.Models.Lessons;

namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class GetLessons
{
    public readonly record struct Request(long ModuleId, long[] LessonIds, LessonKind[] Types, long Cursor, long PageSize);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(IList<LessonDto> Lesson) : Response;

        public sealed record ModuleNotFound : Response;
    }
}