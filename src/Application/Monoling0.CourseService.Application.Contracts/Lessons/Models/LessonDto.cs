using Monoling0.CourseService.Application.Models.Lessons.LessonContents;

namespace Monoling0.CourseService.Application.Contracts.Lessons.Models;

public record LessonDto(long Id, long ModuleId, string LessonType, int LessonNumber, string Name, string Description, LessonContent Content);