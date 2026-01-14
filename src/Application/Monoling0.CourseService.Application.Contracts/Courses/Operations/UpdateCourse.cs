using Monoling0.CourseService.Application.Contracts.Courses.Models;
using Monoling0.CourseService.Application.Models.Courses;

namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class UpdateCourse
{
    public readonly record struct Request(long CourseId, string Name, string Description, CefrLevel Level);

    public readonly record struct Response(CourseDto Courses);
}