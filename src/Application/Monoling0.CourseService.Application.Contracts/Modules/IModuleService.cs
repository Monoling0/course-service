using Monoling0.CourseService.Application.Contracts.Modules.Operations;

namespace Monoling0.CourseService.Application.Contracts.Modules;

public interface IModuleService
{
    Task<CreateModule.Response> AddModuleAsync(CreateModule.Request request, CancellationToken cancellationToken);

    Task UpdateModuleAsync(UpdateModule.Request request, CancellationToken cancellationToken);

    Task DeleteModuleAsync(DeleteModule.Request request, CancellationToken cancellationToken);

    Task SwapModules(SwapModules.Request request, CancellationToken cancellationToken);

    Task<GetModules.Response> GetModuleListAsync(GetModules.Request request, CancellationToken cancellationToken);
}