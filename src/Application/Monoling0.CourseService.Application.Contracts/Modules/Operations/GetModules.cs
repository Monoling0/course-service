using Monoling0.CourseService.Application.Contracts.Modules.Models;

namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class GetModules
{
    public readonly record struct Request(long CourseId, long[] ModuleId, long Cursor, long PageSize);

    public readonly record struct Response(ModuleDto Module);
}