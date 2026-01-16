using Monoling0.CourseService.Application.Models.Lessons.LessonContents.Audio;
using Monoling0.CourseService.Application.Models.Lessons.LessonContents.FlashCards;
using System.Text.Json.Serialization;

namespace Monoling0.CourseService.Application.Models.Lessons.LessonContents;

[JsonDerivedType(typeof(TheoryLessonContent), typeDiscriminator: nameof(TheoryLessonContent))]
[JsonDerivedType(typeof(AudioLessonContent), typeDiscriminator: nameof(AudioLessonContent))]
[JsonDerivedType(typeof(VideoLessonContent), typeDiscriminator: nameof(VideoLessonContent))]
[JsonDerivedType(typeof(FlashCardLessonContent), typeDiscriminator: nameof(FlashCardLessonContent))]
public record LessonContent();