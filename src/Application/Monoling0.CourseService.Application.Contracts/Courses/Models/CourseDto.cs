using Monoling0.CourseService.Application.Models.Courses;

namespace Monoling0.CourseService.Application.Contracts.Courses.Models;

public record CourseDto(long Id, string Name, string Description, string Language, CefrLevel Level, CourseState State);