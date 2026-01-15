using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace Monoling0.CourseService.Infrastructure.Persistence.Migrations;

#pragma warning disable SA1649
[Migration(version: 1727972936, description: "Initial")]
public class Initial : IMigration
{
    public void GetUpExpressions(IMigrationContext context)
    {
        context.Expressions.Add(new ExecuteSqlStatementExpression
        {
            SqlStatement = """
            create type cefr_level as enum
            (
                'a1',
                'a2',
                'b1',
                'b2',
                'c1',
                'c2'
            );

            create type course_state as enum
            (
                'draft',
                'published',
                'unpublished'
            );

            create table courses
            (
                course_id bigint primary key generated always as identity,
                course_name text not null,
                course_description text not null,
                course_language text not null,
                course_level cefr_level not null,
                course_state course_state not null
            );

            create table modules
            (
                module_id bigint primary key generated always as identity,
                course_id bigint not null references courses (course_id),
                module_number int not null,
                module_name text not null,
                module_description text not null,
                constraint unique_module_in_course unique(course_id, module_number)
            );

            create table lesson_types
            (
                lesson_type_id bigint primary key generated always as identity,
                lesson_type_name text not null,
                lesson_type_description text not null,
                lesson_type_experience int not null
            );

            create table lessons
            (
                lesson_id bigint primary key generated always as identity,
                module_id bigint not null references modules (module_id),
                lesson_type_id bigint not null references lesson_types (lesson_type_id),
                lesson_number int not null,
                lesson_name text not null,
                lesson_description text not null,
                lesson_content jsonb not null,
                constraint unique_lesson_in_module unique(module_id, lesson_number)
            );

            create table courses_creators
            (
                course_id bigint not null references courses (course_id),
                creator_id bigint not null,
                constraint course_creator_pkey primary key (course_id, creator_id)
            );
            """,
        });
    }

    public void GetDownExpressions(IMigrationContext context)
    {
        context.Expressions.Add(new ExecuteSqlStatementExpression
        {
            SqlStatement = """
            drop table courses_creators;
            drop table lessons;
            drop table lesson_types;
            drop table modules;
            drop table courses;
            drop type course_state;
            drop type cefr_level;
            """,
        });
    }

    public string ConnectionString => throw new NotSupportedException();
}