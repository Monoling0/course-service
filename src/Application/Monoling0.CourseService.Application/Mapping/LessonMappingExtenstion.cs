using Monoling0.CourseService.Application.Contracts.Lessons.Models;
using Monoling0.CourseService.Application.Models.Lessons;

namespace Monoling0.CourseService.Application.Mapping;

public static class LessonMappingExtenstion
{
    public static LessonDto MapToDto(this Lesson lesson, string lessonType)
        => new LessonDto(lesson.Id, lesson.ModuleId, lessonType, lesson.LessonNumber, lesson.Name, lesson.Description, lesson.Content);
}