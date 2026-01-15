namespace Monoling0.CourseService.Application.Abstractions.Persistence.Queries;

public record LessonQuery(long ModuleId, long[] LessonIds, long[] LessonTypeIds, long Cursor, long PageSize);