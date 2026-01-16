using Monoling0.CourseService.Application.Abstractions.Persistence;
using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Contracts.Modules;
using Monoling0.CourseService.Application.Contracts.Modules.Operations;
using Monoling0.CourseService.Application.Mapping;
using Monoling0.CourseService.Application.Models.Modules;
using System.Transactions;

namespace Monoling0.CourseService.Application;

public class ModuleService : IModuleService
{
    private readonly IPersistenceContext _persistenceContext;

    public ModuleService(IPersistenceContext persistenceContext)
    {
        _persistenceContext = persistenceContext;
    }

    public async Task<CreateModule.Response> AddModuleAsync(CreateModule.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        if ((await _persistenceContext.Courses.FindAsync(request.CourseId, cancellationToken)) == null)
        {
            return new CreateModule.Response.CourseNotFound();
        }

        var module = new CourseModule(request.CourseId, request.Name, request.Description);
        CourseModule savedModule = await _persistenceContext.Modules.CreateAsync(module, cancellationToken);
        transaction.Complete();

        return new CreateModule.Response.Success(savedModule.MapToDto());
    }

    public async Task<UpdateModule.Response> UpdateModuleAsync(UpdateModule.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        CourseModule? module = await _persistenceContext.Modules.FindAsync(request.ModuleId, cancellationToken);
        if (module == null)
        {
            return new UpdateModule.Response.ModuleNotFound();
        }

        var updatedModule = new CourseModule(
            Id: request.ModuleId,
            CourseId: module.CourseId,
            Number: module.Number,
            Name: request.Name != null ? request.Name : module.Name,
            Description: request.Description != null ? request.Description : module.Description);

        await _persistenceContext.Modules.UpdateAsync(updatedModule, cancellationToken);
        transaction.Complete();

        return new UpdateModule.Response.Success();
    }

    public async Task DeleteModuleAsync(DeleteModule.Request request, CancellationToken cancellationToken)
    {
        await _persistenceContext.Modules.DeleteAsync(request.ModuleId, cancellationToken);
    }

    // public async Task<SwapModules.Response> SwapModules(SwapModules.Request request, CancellationToken cancellationToken)
    // {
    //     using var transaction = new TransactionScope(
    //         TransactionScopeOption.Required,
    //         new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
    //         TransactionScopeAsyncFlowOption.Enabled);
    //
    //     CourseModule? firstModule = await _persistenceContext.Modules.FindByNumberAsync(request.CourseId, request.FirstModuleNumber, cancellationToken);
    //     if (firstModule == null)
    //     {
    //         return new SwapModules.Response.ModuleNotFound();
    //     }
    //
    //     CourseModule? secondModule = await _persistenceContext.Modules.FindByNumberAsync(request.CourseId, request.SecondModuleNumber, cancellationToken);
    //     if (secondModule == null)
    //     {
    //         return new SwapModules.Response.ModuleNotFound();
    //     }
    //
    //     transaction.Complete();
    // }
    public async Task<GetModules.Response> GetModuleListAsync(GetModules.Request request, CancellationToken cancellationToken)
    {
        var moduleQuery = new ModuleQuery(
            request.CourseId,
            request.ModuleIds,
            request.Cursor,
            request.PageSize);
        List<CourseModule> result = await _persistenceContext.Modules.QueryAsync(moduleQuery, cancellationToken).ToListAsync(cancellationToken);
        return new GetModules.Response(result.Select(m => m.MapToDto()).ToList());
    }
}