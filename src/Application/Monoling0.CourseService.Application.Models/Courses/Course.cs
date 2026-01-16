namespace Monoling0.CourseService.Application.Models.Courses;

public record Course(string Name, string Description, string Language, CefrLevel Level, CourseState State, long Id = 0);