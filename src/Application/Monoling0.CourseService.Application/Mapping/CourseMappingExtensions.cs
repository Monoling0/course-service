using Monoling0.CourseService.Application.Contracts.Courses.Models;
using Monoling0.CourseService.Application.Models.Courses;

namespace Monoling0.CourseService.Application.Mapping;

public static class CourseMappingExtensions
{
    public static CourseDto MapToDto(this Course course)
        => new CourseDto(course.Id,  course.Name, course.Description, course.Language, course.Level, course.State);
}