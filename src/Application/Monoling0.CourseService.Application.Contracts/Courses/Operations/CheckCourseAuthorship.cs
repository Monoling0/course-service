namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class CheckCourseAuthorship
{
    public readonly record struct Request(long CourseId, long UserId);

    public readonly record struct Response(bool CheckResult);
}