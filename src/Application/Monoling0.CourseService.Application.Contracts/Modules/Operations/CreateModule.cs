using Monoling0.CourseService.Application.Contracts.Modules.Models;

namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class CreateModule
{
    public readonly record struct Request(long CourseId, string Name, string Description);

    public readonly record struct Response(ModuleDto Module);
}