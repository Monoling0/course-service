using Monoling0.CourseService.Application.Contracts.Modules.Models;

namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class UpdateModule
{
    public readonly record struct Request(long ModuleId, string Name, string Description);

    public readonly record struct Response(IAsyncEnumerable<ModuleDto> Module);
}