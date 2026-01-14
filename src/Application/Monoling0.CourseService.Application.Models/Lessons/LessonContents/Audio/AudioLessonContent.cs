namespace Monoling0.CourseService.Application.Models.Lessons.LessonContents.Audio;

public record AudioLessonContent(IList<AudioItem> AudioItems) : LessonContent;