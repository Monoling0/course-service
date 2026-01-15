using Monoling0.CourseService.Application.Abstractions.Persistence;
using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Contracts.Lessons;
using Monoling0.CourseService.Application.Contracts.Lessons.Models;
using Monoling0.CourseService.Application.Contracts.Lessons.Operations;
using Monoling0.CourseService.Application.Mapping;
using Monoling0.CourseService.Application.Models.Lessons;
using System.Transactions;

namespace Monoling0.CourseService.Application;

public class LessonService : ILessonService
{
    private readonly IPersistenceContext _persistenceContext;

    public LessonService(IPersistenceContext persistenceContext)
    {
        _persistenceContext = persistenceContext;
    }

    public async Task<CreateLesson.Response> AddLessonAsync(CreateLesson.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        if ((await _persistenceContext.Modules.FindAsync(request.ModuleId, cancellationToken)) == null)
        {
            return new CreateLesson.Response.ModuleNotFound();
        }

        long lessonTypeId = await _persistenceContext.LessonTypes.GetIdAsync(request.Type.ToString(), cancellationToken);
        var lesson = new Lesson(request.ModuleId, lessonTypeId, request.Name, request.Description, request.Content);
        Lesson createdLesson = await _persistenceContext.Lessons.CreateAsync(lesson, cancellationToken);
        transaction.Complete();

        return new CreateLesson.Response.Success(createdLesson.MapToDto(request.Type.ToString()));
    }

    public async Task<UpdateLesson.Response> UpdateLessonAsync(UpdateLesson.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Lesson? lesson = await _persistenceContext.Lessons.FindAsync(request.LessonId, cancellationToken);
        if (lesson == null)
        {
            return new UpdateLesson.Response.LessonNotFound();
        }

        var updatedLesson = new Lesson(
            Id: request.LessonId,
            ModuleId: lesson.ModuleId,
            LessonNumber: lesson.LessonNumber,
            LessonTypeId: lesson.LessonTypeId,
            Name: request.Name != null ? request.Name : lesson.Name,
            Description: request.Description != null ? request.Description : lesson.Description,
            Content: request.Content != null ? request.Content : lesson.Content);

        await _persistenceContext.Lessons.UpdateAsync(updatedLesson, cancellationToken);
        transaction.Complete();

        return new UpdateLesson.Response.Success();
    }

    public async Task DeleteLessonAsync(DeleteLesson.Request request, CancellationToken cancellationToken)
    {
        await _persistenceContext.Lessons.DeleteAsync(request.LessonId, cancellationToken);
    }

    public async Task<GetLesson.Response> GetLessonListAsync(GetLesson.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        int size = request.Types.Length;
        long[] lessonTypes = new long[size];
        for (int i = 0; i < size; i++)
        {
            lessonTypes[i] = await _persistenceContext.LessonTypes.GetIdAsync(request.Types[i].ToString(), cancellationToken);
        }

        var lessonQuery = new LessonQuery(
            request.ModuleId,
            request.LessonIds,
            lessonTypes,
            request.Cursor,
            request.PageSize);

        IList<Lesson> lessons = await _persistenceContext.Lessons.QueryAsync(lessonQuery, cancellationToken).ToListAsync(cancellationToken);

        var response = new List<LessonDto>();
        foreach (Lesson lesson in lessons)
        {
            string typeName = await _persistenceContext.LessonTypes.GetNameAsync(lesson.LessonTypeId, cancellationToken);
            response.Add(lesson.MapToDto(typeName));
        }

        transaction.Complete();

        return new GetLesson.Response.Success(response);
    }

    public async Task<GetLessonExperience.Response> GetLessonExperienceAsync(GetLessonExperience.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Lesson? lesson = await _persistenceContext.Lessons.FindAsync(request.LessonId, cancellationToken);
        if (lesson == null)
        {
            return new GetLessonExperience.Response.LessonNotFound();
        }

        int experience = await _persistenceContext.LessonTypes.GetExperienceAsync(lesson.LessonTypeId, cancellationToken);
        transaction.Complete();

        return new GetLessonExperience.Response.Success(experience);
    }
}