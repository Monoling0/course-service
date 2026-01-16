using Monoling0.CourseService.Application.Contracts.Lessons.Operations;

namespace Monoling0.CourseService.Application.Contracts.Lessons;

public interface ILessonService
{
    Task<CreateLesson.Response> AddLessonAsync(CreateLesson.Request request, CancellationToken cancellationToken);

    Task<UpdateLesson.Response> UpdateLessonAsync(UpdateLesson.Request request, CancellationToken cancellationToken);

    Task DeleteLessonAsync(DeleteLesson.Request request, CancellationToken cancellationToken);

    // Task SwapLessons(SwapLessons.Request request, CancellationToken cancellationToken);
    Task<GetLessons.Response> GetLessonListAsync(GetLessons.Request request, CancellationToken cancellationToken);

    Task<GetLessonExperience.Response> GetLessonExperienceAsync(GetLessonExperience.Request request, CancellationToken cancellationToken);
}