using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;
using Monoling0.CourseService.Application.Models.Courses;
using Npgsql;
using System.Runtime.CompilerServices;

namespace Monoling0.CourseService.Infrastructure.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public CourseRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Course> CreateAsync(Course course, CancellationToken cancellationToken)
    {
        const string sql = """
        insert into courses (course_name, course_description, course_language, course_level, course_state)
        values (:name, :description, :language, :level, :state) returning course_id;
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("name", course.Name),
                new NpgsqlParameter("description", course.Description),
                new NpgsqlParameter("language", course.Language),
                new NpgsqlParameter("level", course.Level),
                new NpgsqlParameter("state", course.State),
            },
        };

        object? id = await command.ExecuteScalarAsync(cancellationToken);
        if (id == null)
        {
            throw new Exception("Cannot insert course into db.");
        }

        return new Course(
            Id: (long)id,
            Name: course.Name,
            Description: course.Description,
            Language: course.Language,
            Level: course.Level,
            State: course.State);
    }

    public async Task<Course?> FindAsync(long id, CancellationToken cancellationToken)
    {
        const string sql = """
        select course_id, course_name, course_description, course_language, course_level, course_state
        from courses
        where
            (course_id = :id)
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
            return null;
        }

        return new Course(
            Id: reader.GetInt64(0),
            Name: reader.GetString(1),
            Description: reader.GetString(2),
            Language: reader.GetString(3),
            Level: reader.GetFieldValue<CefrLevel>(4),
            State: reader.GetFieldValue<CourseState>(5));
    }

    public async Task UpdateAsync(Course course, CancellationToken cancellationToken)
    {
        const string sql = """
        update courses
        set course_name = :name,
            course_description = :description,
            course_language = :language,
            course_level = :level,
            course_state = :state
        where
            (course_id = :id)
        """;
        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("id", course.Id),
                new NpgsqlParameter("name", course.Name),
                new NpgsqlParameter("description", course.Description),
                new NpgsqlParameter("language", course.Language),
                new NpgsqlParameter("level", course.Level),
                new NpgsqlParameter("state", course.State),
            },
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(long courseId, CancellationToken cancellationToken)
    {
        const string sql = """
        delete from courses
        where
         (course_id = :id)
        """;
        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("id", courseId),
            },
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async IAsyncEnumerable<Course> QueryAsync(CourseQuery query, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql = """
        select course_id, course_name, course_description, course_language, course_level, course_state
        from courses
        where
          (cardinality(:ids) = 0 or course_id = any(:ids))
          and (:course_language is null or course_language = :language)
          and (:course_level is null or course_level = :level)
          and (:course_state is null or course_state = :state)
          and course_id > :cursor
        order by course_id asc
        limit :page_size;
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("ids", query.CourseIds),
                new NpgsqlParameter("language", query.Language),
                new NpgsqlParameter("level", query.CourseLevel),
                new NpgsqlParameter("state", query.CourseState),
                new NpgsqlParameter("cursor", query.Cursor),
                new NpgsqlParameter("page_size", query.PageSize),
            },
        };

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            yield return new Course(
                Id: reader.GetInt64(0),
                Name: reader.GetString(1),
                Description: reader.GetString(2),
                Language: reader.GetString(3),
                Level: reader.GetFieldValue<CefrLevel>(4),
                State: reader.GetFieldValue<CourseState>(5));
        }
    }
}