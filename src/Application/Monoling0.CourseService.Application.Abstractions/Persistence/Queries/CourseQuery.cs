using Monoling0.CourseService.Application.Models.Courses;

namespace Monoling0.CourseService.Application.Abstractions.Persistence.Queries;

public record CourseQuery(
    long[] CourseIds,
    string? Language,
    CefrLevel? CourseLevel,
    CourseState? CourseState,
    long Cursor,
    long PageSize);