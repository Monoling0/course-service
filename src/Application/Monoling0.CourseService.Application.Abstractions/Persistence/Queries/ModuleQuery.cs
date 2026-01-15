namespace Monoling0.CourseService.Application.Abstractions.Persistence.Queries;

public record class ModuleQuery(long CourseId, long[] ModuleIds, long Cursor, long PageSize);