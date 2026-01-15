using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;
using Monoling0.CourseService.Application.Models.Lessons;
using Monoling0.CourseService.Application.Models.Lessons.LessonContents;
using Npgsql;
using NpgsqlTypes;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Monoling0.CourseService.Infrastructure.Persistence.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public LessonRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Lesson> CreateAsync(Lesson lesson, CancellationToken cancellationToken)
    {
        const string sql = """
        insert into lessons (module_id, lesson_type_id, lesson_number, lesson_name, lesson_description, lesson_content)
        values (:module_id, :lesson_type_id, :lesson_number, :lesson_name, :lesson_description, lesson_content) returning lesson_id;
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("module_id", lesson.ModuleId),
                new NpgsqlParameter("lesson_type_id", lesson.LessonTypeId),
                new NpgsqlParameter("lesson_number", lesson.LessonNumber),
                new NpgsqlParameter("lesson_name", lesson.Name),
                new NpgsqlParameter("lesson_description", lesson.Description),
                new NpgsqlParameter("content", NpgsqlDbType.Jsonb)
                {
                    Value = JsonSerializer.Serialize(lesson.Content),
                },
            },
        };

        object? id = await command.ExecuteScalarAsync(cancellationToken);
        if (id == null)
        {
            throw new Exception("Cannot insert course into db.");
        }

        return new Lesson(
            Id: (long)id,
            ModuleId: lesson.ModuleId,
            LessonTypeId: lesson.LessonTypeId,
            LessonNumber: lesson.LessonNumber,
            Name: lesson.Name,
            Description: lesson.Description,
            Content: lesson.Content);
    }

    public async Task<Lesson?> FindAsync(long id, CancellationToken cancellationToken)
    {
        const string sql = """
        select lesson_id, module_id, lesson_type_id, lesson_number, lesson_name, lesson_description, lesson_content
        from lessons
        where
           (lesson_id = :id)
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

        return new Lesson(
            Id: reader.GetInt64(0),
            ModuleId: reader.GetInt64(1),
            LessonTypeId: reader.GetInt64(2),
            LessonNumber: reader.GetInt32(3),
            Name: reader.GetString(4),
            Description: reader.GetString(5),
            Content: JsonSerializer.Deserialize<LessonContent>(reader.GetString(6)) ?? throw new Exception("Cannot deserialize lesson content"));
    }

    public async Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken)
    {
        const string sql = """
        update lessons
        set lesson_name = :name,
            lesson_description = :description,
            lesson_content = :content
        where
          (lesson_id = :id)
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("name", lesson.Name),
                new NpgsqlParameter("description", lesson.Description),
                new NpgsqlParameter("content", NpgsqlDbType.Jsonb)
                {
                    Value = JsonSerializer.Serialize(lesson.Content),
                },
            },
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(long lessonId, CancellationToken cancellationToken)
    {
        const string sql = """
        delete from lessons
        where
           (lesson_id = :id);
        update lessons
        """;
        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("id", lessonId),
            },
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async IAsyncEnumerable<Lesson> QueryAsync(LessonQuery query, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql = """
        select lesson_id, module_id, lesson_type_id, lesson_number, lesson_name, lesson_description, lesson_content
        from lessons
        where
         (cardinality(:ids) = 0 or lesson_id = any(:ids))
         (cardinality(:type_ids) = 0 or lesson_type_id = any(:type_ids))
         and module_id = :module_id
         and lesson_id > :cursor
        order by lesson_id asc
        limit :page_size;
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("ids", query.LessonIds),
                new NpgsqlParameter("type_ids", query.LessonTypeIds),
                new NpgsqlParameter("module_id", query.ModuleId),
                new NpgsqlParameter("cursor", query.Cursor),
                new NpgsqlParameter("page_size", query.PageSize),
            },
        };

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            yield return new Lesson(
                Id: reader.GetInt64(0),
                ModuleId: reader.GetInt64(1),
                LessonTypeId: reader.GetInt64(2),
                LessonNumber: reader.GetInt32(3),
                Name: reader.GetString(4),
                Description: reader.GetString(5),
                Content: JsonSerializer.Deserialize<LessonContent>(reader.GetString(6)) ?? throw new Exception("Cannot deserialize lesson content"));
        }
    }
}