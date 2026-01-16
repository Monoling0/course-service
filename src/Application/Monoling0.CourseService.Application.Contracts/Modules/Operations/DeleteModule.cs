namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class DeleteModule
{
    public readonly record struct Request(long ModuleId);
}