using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;
using Npgsql;

namespace Monoling0.CourseService.Infrastructure.Persistence.Repositories;

public class LessonTypeRepository : ILessonTypeRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public LessonTypeRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<int> GetExperienceAsync(long lessonTypeId, CancellationToken cancellationToken)
    {
        const string sql = """
        select lesson_type_experience
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
            throw new Exception($"There is no experience for this lesson type: {lessonTypeId}");
        }

        return reader.GetInt32(0);
    }

    public async Task<long> GetIdAsync(string lessonTypeName, CancellationToken cancellationToken)
    {
        const string sql = """
                           select lesson_type_id
                           from lesson_type_name = :name
                           where
                             (lesson_type_name = :name)
                           """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("name", lessonTypeName),
            },
        };

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            throw new Exception($"There is no lesson type with such name: {lessonTypeName}");
        }

        return reader.GetInt64(0);
    }

    public async Task<string> GetNameAsync(long id, CancellationToken cancellationToken)
    {
        const string sql = """
                           select lesson_type_name
                           from lesson_type_name = :name
                           where
                             (lesson_type_id = :id)
                           """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("id", id),
            },
        };

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            throw new Exception($"There is no lesson type with such id: {id}");
        }

        return reader.GetString(0);
    }
}