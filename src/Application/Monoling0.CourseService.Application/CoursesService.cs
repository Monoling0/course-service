using Monoling0.CourseService.Application.Abstractions.Persistence;
using Monoling0.CourseService.Application.Abstractions.Persistence.Queries;
using Monoling0.CourseService.Application.Contracts.Courses;
using Monoling0.CourseService.Application.Contracts.Courses.Operations;
using Monoling0.CourseService.Application.Mapping;
using Monoling0.CourseService.Application.Models.Courses;
using System.Transactions;

namespace Monoling0.CourseService.Application;

public class CoursesService : ICourseService
{
    private readonly IPersistenceContext _persistenceContext;

    public CoursesService(IPersistenceContext persistenceContext)
    {
        _persistenceContext = persistenceContext;
    }

    public async Task<CreateCourse.Response> CreateAsync(CreateCourse.Request request, CancellationToken cancellationToken)
    {
        var course = new Course(request.Name, request.Description, request.Language,  request.Level, CourseState.Draft);
        Course createdCourse = await _persistenceContext.Courses.CreateAsync(course, cancellationToken);
        return new CreateCourse.Response(createdCourse.MapToDto());
    }

    public async Task<UpdateCourse.Result> UpdateAsync(UpdateCourse.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Course? course = await _persistenceContext.Courses.FindAsync(request.CourseId, cancellationToken);
        if (course == null)
        {
            return new UpdateCourse.Result.CourseNotFound();
        }

        var updatedCourse = new Course(
            Id: course.Id,
            Name: request.Name != null ? request.Name : course.Name,
            Description: request.Description != null ? request.Description : course.Description,
            Language: course.Language,
            Level: request.Level != null ? request.Level.Value : course.Level,
            State: course.State);

        await _persistenceContext.Courses.UpdateAsync(updatedCourse, cancellationToken);

        transaction.Complete();

        return new UpdateCourse.Result.Success();
    }

    public async Task DeleteAsync(DeleteCourse.Request request, CancellationToken cancellationToken)
    {
        await _persistenceContext.Courses.DeleteAsync(request.CourseId,  cancellationToken);
    }

    public async Task<PublishCourse.Result> PublishAsync(PublishCourse.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Course? course = await _persistenceContext.Courses.FindAsync(request.CourseId, cancellationToken);
        if (course == null)
        {
            return new PublishCourse.Result.CourseNotFound();
        }

        if (course.State == CourseState.Published)
        {
            return new PublishCourse.Result.AlreadyPublished();
        }

        var updatedCourse = new Course(
            Id: course.Id,
            Name: course.Name,
            Description: course.Description,
            Language: course.Language,
            Level: course.Level,
            State: CourseState.Published);

        await _persistenceContext.Courses.UpdateAsync(updatedCourse, cancellationToken);

        transaction.Complete();

        return new PublishCourse.Result.Success();
    }

    public async Task<UnpublishCourse.Result> UnpublishAsync(UnpublishCourse.Request request, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);

        Course? course = await _persistenceContext.Courses.FindAsync(request.CourseId, cancellationToken);
        if (course == null)
        {
            return new UnpublishCourse.Result.CourseNotFound();
        }

        if (course.State != CourseState.Published)
        {
            return new UnpublishCourse.Result.CannotUnpublish();
        }

        var updatedCourse = new Course(
            Id: course.Id,
            Name: course.Name,
            Description: course.Description,
            Language: course.Language,
            Level: course.Level,
            State: CourseState.Unpublished);

        await _persistenceContext.Courses.UpdateAsync(updatedCourse, cancellationToken);

        transaction.Complete();

        return new UnpublishCourse.Result.Success();
    }

    public async Task<GetCourses.Response> GetCourseListAsync(GetCourses.Request request, CancellationToken cancellationToken)
    {
        var courseQuery = new CourseQuery(
            request.CourseIds,
            request.Language,
            request.Level,
            request.State,
            request.Cursor,
            request.PageSize);
        List<Course> result = await _persistenceContext.Courses.QueryAsync(courseQuery, cancellationToken).ToListAsync(cancellationToken);
        return new GetCourses.Response(result.Select(c => c.MapToDto()).ToList());
    }

    public async Task<CheckCourseAuthorship.Response> CheckCourseAuthorship(CheckCourseAuthorship.Request request, CancellationToken cancellationToken)
    {
        List<long> authors = await _persistenceContext.CourseCreators.GetCourseCreatorsAsync(request.CourseId, cancellationToken).ToListAsync(cancellationToken);

        return new CheckCourseAuthorship.Response(authors.Contains(request.UserId));
    }

    public async Task AddCourseCreator(AddCourseCreator.Request request, CancellationToken cancellationToken)
    {
        await _persistenceContext.CourseCreators.CreateAsync(request.CourseId, request.UserId, cancellationToken);
    }
}