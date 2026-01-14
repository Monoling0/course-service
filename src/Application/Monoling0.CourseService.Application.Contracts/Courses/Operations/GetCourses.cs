using Monoling0.CourseService.Application.Contracts.Courses.Models;
using Monoling0.CourseService.Application.Models.Courses;

namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class GetCourses
{
    public readonly record struct Request(
        long[] CourseIds,
        string? Language,
        CefrLevel? Level,
        CourseState? State,
        long Cursor,
        long PageSize);

    public readonly record struct Response(IAsyncEnumerable<CourseDto> Courses);
}