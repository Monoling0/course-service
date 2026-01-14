using Grpc.Core;
using Modules.CourseService.Contracts;

namespace Monoling0.CourseService.Presentation.Grpc.Controllers;

public class ModuleController : ModuleService.ModuleServiceBase
{
    public override Task<AddModuleResponse> AddModule(AddModuleRequest request, ServerCallContext context)
    {
        return base.AddModule(request, context);
    }

    public override Task<UpdateModuleResponse> UpdateModule(UpdateModuleRequest request, ServerCallContext context)
    {
        return base.UpdateModule(request, context);
    }

    public override Task<DeleteModuleResponse> DeleteModule(DeleteModuleRequest request, ServerCallContext context)
    {
        return base.DeleteModule(request, context);
    }

    public override Task<SwapModulesResponse> SwapModules(SwapModulesRequest request, ServerCallContext context)
    {
        return base.SwapModules(request, context);
    }

    public override Task<GetModuleListResponse> GetModuleList(GetModuleListRequest request, ServerCallContext context)
    {
        return base.GetModuleList(request, context);
    }
}