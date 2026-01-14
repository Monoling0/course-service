namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class UpdateModule
{
    public readonly record struct Request(long ModuleId, string Name, string Description);
}