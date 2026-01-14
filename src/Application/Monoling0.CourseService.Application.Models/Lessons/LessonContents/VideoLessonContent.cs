namespace Monoling0.CourseService.Application.Models.Lessons.LessonContents;

public record VideoLessonContent(IList<string> VideoUrls) : LessonContent;