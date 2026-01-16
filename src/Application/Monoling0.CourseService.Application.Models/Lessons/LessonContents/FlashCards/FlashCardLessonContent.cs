namespace Monoling0.CourseService.Application.Models.Lessons.LessonContents.FlashCards;

public record FlashCardLessonContent(IList<FlashCard> FlashCards) : LessonContent;