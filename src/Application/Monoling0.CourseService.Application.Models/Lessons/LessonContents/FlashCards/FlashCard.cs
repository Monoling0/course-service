namespace Monoling0.CourseService.Application.Models.Lessons.LessonContents.FlashCards;

public record FlashCard(string Word, string Translation, string? ExampleSentence, IList<string> Synonyms);