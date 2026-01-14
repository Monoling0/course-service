namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class SwapModules
{
    public readonly record struct Request(long CourseId, int FirstModuleNumber, int SecondModuleNumber);
}