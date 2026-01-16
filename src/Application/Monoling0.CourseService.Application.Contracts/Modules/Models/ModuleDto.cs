namespace Monoling0.CourseService.Application.Contracts.Modules.Models;

public record ModuleDto(long Id, long CourseId, int Number, string Name, string Description);