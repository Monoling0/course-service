namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class DeleteCourse
{
    public readonly record struct Request(long CourseId);
}