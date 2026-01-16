using Monoling0.CourseService.Application.Contracts.Modules.Models;
using Monoling0.CourseService.Application.Models.Modules;

namespace Monoling0.CourseService.Application.Mapping;

public static class ModuleMappingExtension
{
    public static ModuleDto MapToDto(this CourseModule module)
        => new ModuleDto(module.Id, module.CourseId, module.Number, module.Name, module.Description);
}
