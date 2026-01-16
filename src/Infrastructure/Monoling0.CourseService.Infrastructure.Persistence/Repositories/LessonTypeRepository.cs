using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;
using Monoling0.CourseService.Application.Models.Lessons;
using Npgsql;

namespace Monoling0.CourseService.Infrastructure.Persistence.Repositories;

public class LessonTypeRepository : ILessonTypeRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public LessonTypeRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<LessonType> GetAsync(long lessonTypeId, CancellationToken cancellationToken)
    {
        const string sql = """
        select lesson_type_id, lesson_type_name, lesson_type_description, lesson_type_experience
        from lesson_types
        where
          (lesson_type_id = :id)
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("id", lessonTypeId),
            },
        };

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            throw new Exception($"Lesson type with id {lessonTypeId} does not exist.");
        }

        return new LessonType(
            Id: reader.GetInt64(0),
            Name: reader.GetString(1),
            Description: reader.GetString(2),
            Experience: reader.GetInt32(3));
    }
}