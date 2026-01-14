namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class UnpublishCourse
{
    public readonly record struct Request(long CourseId, string? Reason, long UnpublishedByUserId);
}