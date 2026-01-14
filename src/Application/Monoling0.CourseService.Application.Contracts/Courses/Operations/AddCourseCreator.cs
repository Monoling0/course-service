namespace Monoling0.CourseService.Application.Contracts.Courses.Operations;

public static class AddCourseCreator
{
    public readonly record struct Request(long CourseId, long NewCourseCreatorId, long RequestedByUserId);
}