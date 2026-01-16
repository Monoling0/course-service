using Monoling0.CourseService.Application.Contracts.Courses.Models;
using Monoling0.CourseService.Application.Models.Courses;

namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class CreateCourse
{
    public readonly record struct Request(string Name, string Description, string Language, CefrLevel Level, long CreatedBy);

    public readonly record struct Response(CourseDto Course);
}