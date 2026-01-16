using Modules.CourseService.Contracts;
using Monoling0.CourseService.Application.Contracts.Modules.Operations;

namespace Monoling0.CourseService.Presentation.Grpc.Extensions;

public static class ModuleMappingExtension
{
    public static CreateModule.Request MapToOperation(this AddModuleRequest request)
        => new CreateModule.Request(request.CourseId, request.Name, request.Description);

    public static GetModules.Request MapToOperation(this GetModuleListRequest request)
        => new GetModules.Request(request.CourseIds, request.ModuleIds.ToArray(),  request.Cursor, request.PageSize);
}