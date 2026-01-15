using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Abstractions.Persistence.Repositories;
using Monoling0.CourseService.Application.Models.Modules;
using Npgsql;
using System.Runtime.CompilerServices;

namespace Monoling0.CourseService.Infrastructure.Persistence.Repositories;

public class ModuleRepository : IModuleRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public ModuleRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<CourseModule> CreateAsync(CourseModule courseModule, CancellationToken cancellationToken)
    {
        const string sql = """
        insert into modules (course_id, module_number module_name, module_description)
        values (
                :course_id,
                (
                    select count(*) + 1
                    from modules
                    where course_id = :course_id
                ),
                :name,
                :description)
        returning module_id;
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("course_id", courseModule.CourseId),
                new NpgsqlParameter("name", courseModule.Name),
                new NpgsqlParameter("description", courseModule.Description),
            },
        };

        object? id = await command.ExecuteScalarAsync(cancellationToken);
        if (id == null)
        {
            throw new Exception("Cannot insert module into db.");
        }

        return new CourseModule(
            Id: (long)id,
            CourseId: courseModule.CourseId,
            Number: courseModule.Number,
            Name: courseModule.Name,
            Description: courseModule.Description);
    }

    public async Task<CourseModule?> FindAsync(long id, CancellationToken cancellationToken)
    {
        const string sql = """
        select module_id, course_id, module_number, module_name, module_description
        from modules
        where
           (module_id = :id)
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

        return new CourseModule(
            Id: reader.GetInt64(0),
            CourseId: reader.GetInt64(1),
            Number: reader.GetInt32(2),
            Name: reader.GetString(3),
            Description: reader.GetString(4));
    }

    public async Task<CourseModule?> FindByNumberAsync(long courseId, int moduleNumber, CancellationToken cancellationToken)
    {
        const string sql = """
        select module_id, course_id, module_number, module_name, module_description
        from modules
        where
          (course_id = :id and module_number = :number)
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("id", courseId),
                new NpgsqlParameter("number", moduleNumber),
            },
        };

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new CourseModule(
            Id: reader.GetInt64(0),
            CourseId: reader.GetInt64(1),
            Number: reader.GetInt32(2),
            Name: reader.GetString(3),
            Description: reader.GetString(4));
    }

    public async Task UpdateAsync(CourseModule courseModule, CancellationToken cancellationToken)
    {
        const string sql = """
        update modules
        set course_id = :course_id,
            module_number = :number,
            module_name = :name,
            module_description = :description
        where
           (module_id = :id)
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("course_id", courseModule.CourseId),
                new NpgsqlParameter("number", courseModule.Number),
                new NpgsqlParameter("name", courseModule.Name),
                new NpgsqlParameter("description", courseModule.Description),
            },
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task DeleteAsync(long moduleId, CancellationToken cancellationToken)
    {
        const string sql = """
        delete from modules
        where
            (module_id = :id)
        """;
        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("id", moduleId),
            },
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async IAsyncEnumerable<CourseModule> QueryAsync(ModuleQuery query, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql = """
        select module_id, course_id, module_number, module_name, module_description
        from modules
        where
         (cardinality(:ids) = 0 or module_id = any(:ids))
         and course_id = :course_id
         and module_id > :cursor
        order by course_id asc
        limit :page_size;
        """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        await using var command = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new NpgsqlParameter("course_id", query.CourseId),
                new NpgsqlParameter("ids", query.ModuleIds),
                new NpgsqlParameter("cursor", query.Cursor),
                new NpgsqlParameter("page_size", query.PageSize),
            },
        };

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            yield return new CourseModule(
                Id: reader.GetInt64(0),
                CourseId: reader.GetInt64(1),
                Number: reader.GetInt32(2),
                Name: reader.GetString(3),
                Description: reader.GetString(4));
        }
    }
}