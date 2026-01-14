namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class PublishCourse
{
    public readonly record struct Request(long CourseId, long PublishedByUserId);
}