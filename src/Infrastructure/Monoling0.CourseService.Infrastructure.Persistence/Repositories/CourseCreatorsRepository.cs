using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;
using Npgsql;
using System.Runtime.CompilerServices;

namespace Monoling0.CourseService.Infrastructure.Persistence.Repositories;

public class CourseCreatorsRepository : ICourseCreatorRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public CourseCreatorsRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task CreateAsync(long courseId, long userId, CancellationToken cancellationToken)
    {
        const string sql = """
        insert into courses_creators (course_id, creator_id)
        values (:courseId, :creatorId);
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("courseId", courseId),
                new NpgsqlParameter("creatorId", userId),
            },
        };
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async IAsyncEnumerable<long> GetCourseCreatorsAsync(long courseId, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql = """
        select courses_creators
        from courses
        where course_id = :courseId;
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("courseId", courseId),
            },
        };

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            yield return reader.GetInt64(0);
        }
    }
}