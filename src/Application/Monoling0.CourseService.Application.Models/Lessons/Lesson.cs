using Monoling0.CourseService.Application.Models.Lessons.LessonContents;

namespace Monoling0.CourseService.Application.Models.Lessons;

public record Lesson(long ModuleId, long LessonTypeId, int LessonNumber, string Name, string Description, LessonContent Content, long Id = 0);