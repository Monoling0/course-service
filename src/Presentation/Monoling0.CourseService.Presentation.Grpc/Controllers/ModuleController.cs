using Grpc.Core;
using Modules.CourseService.Contracts;
using Monoling0.CourseService.Application.Contracts.Modules;
using Monoling0.CourseService.Application.Contracts.Modules.Operations;
using Monoling0.CourseService.Presentation.Grpc.Extensions;

namespace Monoling0.CourseService.Presentation.Grpc.Controllers;

public class ModuleController : ModuleService.ModuleServiceBase
{
    private readonly IModuleService _moduleService;

    public ModuleController(IModuleService moduleService)
    {
        _moduleService = moduleService;
    }

    public override async Task<AddModuleResponse> AddModule(AddModuleRequest request, ServerCallContext context)
    {
        CreateModule.Response response = await _moduleService.AddModuleAsync(request.MapToOperation(), context.CancellationToken);
        return response switch
        {
            CreateModule.Response.CourseNotFound => throw new RpcException(
                new Status(StatusCode.NotFound, $"Course with id {request.CourseId} not found")),

            CreateModule.Response.Success success => new AddModuleResponse()
                { Module = success.Module.MapToMessage() },
            _ => throw new Exception("Unknown result"),
        };
    }

    public override Task<UpdateModuleResponse> UpdateModule(UpdateModuleRequest request, ServerCallContext context)
    {
        return base.UpdateModule(request, context);
    }

    public override async Task<GetModuleListResponse> GetModuleList(GetModuleListRequest request, ServerCallContext context)
    {
        GetModules.Response response = await _moduleService.GetModuleListAsync(request.MapToOperation(), context.CancellationToken);
        return new GetModuleListResponse()
        {
            Modules = { response.Modules.Select(m => m.MapToMessage()) },
        };
    }
}