namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class SwapLessons
{
    public readonly record struct Request(long ModuleId, int FirstLessonNumber, int SecondLessonNumber);
}