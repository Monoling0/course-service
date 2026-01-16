namespace Monoling0.CourseService.Application.Contracts.Lessons.Operations;

public static class DeleteLesson
{
    public readonly record struct Request(long LessonId);
}