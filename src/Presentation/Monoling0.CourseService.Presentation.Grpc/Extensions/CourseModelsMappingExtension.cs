using Lessons.CourseService.Contracts;
using Monoling0.CourseService.Application.Contracts.Courses.Models;
using Monoling0.CourseService.Application.Contracts.Lessons.Models;
using Monoling0.CourseService.Application.Contracts.Modules.Models;
using Monoling0.CourseService.Application.Models.Courses;
using Monoling0.CourseService.Application.Models.Lessons;
using Monoling0.CourseService.Application.Models.Lessons.LessonContents;
using Monoling0.CourseService.Application.Models.Lessons.LessonContents.Audio;
using Monoling0.CourseService.Application.Models.Lessons.LessonContents.FlashCards;
using GrpcContracts = Courses.CourseService.Contracts;

namespace Monoling0.CourseService.Presentation.Grpc.Extensions;

public static class CourseModelsMappingExtension
{
    public static CefrLevel MapToModel(this GrpcContracts.CefrLevel level)
    {
        return level switch
        {
            GrpcContracts.CefrLevel.A1 => CefrLevel.A1,
            GrpcContracts.CefrLevel.A2 => CefrLevel.A2,
            GrpcContracts.CefrLevel.B1 => CefrLevel.B1,
            GrpcContracts.CefrLevel.B2 => CefrLevel.B2,
            GrpcContracts.CefrLevel.C1 => CefrLevel.C1,
            GrpcContracts.CefrLevel.C2 => CefrLevel.C2,
            _ => throw new Exception($"Unknown level {level}"),
        };
    }

    public static GrpcContracts.CefrLevel MapToMessage(this CefrLevel level)
    {
        return level switch
        {
            CefrLevel.A1 => GrpcContracts.CefrLevel.A1,
            CefrLevel.A2 => GrpcContracts.CefrLevel.A2,
            CefrLevel.B1 => GrpcContracts.CefrLevel.B1,
            CefrLevel.B2 => GrpcContracts.CefrLevel.B2,
            CefrLevel.C1 => GrpcContracts.CefrLevel.C1,
            CefrLevel.C2 => GrpcContracts.CefrLevel.C2,
            _ => throw new Exception($"Unknown level {level}"),
        };
    }

    public static CourseState MapToModel(this GrpcContracts.CourseState state)
    {
        return state switch
        {
            GrpcContracts.CourseState.Published => CourseState.Published,
            GrpcContracts.CourseState.Unpublished => CourseState.Unpublished,
            GrpcContracts.CourseState.Draft => CourseState.Draft,
            _ => throw new Exception($"Unknown state {state}"),
        };
    }

    public static LessonKind MapToModel(this GrpcContracts.LessonType kind)
    {
        return kind switch
        {
            GrpcContracts.LessonType.Theory => LessonKind.Theory,
            GrpcContracts.LessonType.Video => LessonKind.Video,
            GrpcContracts.LessonType.Audio => LessonKind.Audio,
            GrpcContracts.LessonType.Flashcards => LessonKind.FlashCards,
            _ => throw new Exception($"Unknown lesson type {kind}"),
        };
    }

    public static GrpcContracts.CourseState MapToMessage(this CourseState state)
    {
        return state switch
        {
            CourseState.Draft => GrpcContracts.CourseState.Draft,
            CourseState.Published => GrpcContracts.CourseState.Published,
            CourseState.Unpublished => GrpcContracts.CourseState.Unpublished,
            _ => throw new Exception($"Unknown state {state}"),
        };
    }

    public static GrpcContracts.Course MapToMessage(this CourseDto course)
        => new GrpcContracts.Course
        {
            Id = course.Id,
            Description = course.Description,
            Language = course.Language,
            Level = course.Level.MapToMessage(),
            State = course.State.MapToMessage(),
        };

    public static GrpcContracts.Module MapToMessage(this ModuleDto module)
        => new GrpcContracts.Module
        {
            Id = module.Id,
            CourseId = module.CourseId,
            Number = module.Number,
            Name = module.Name,
            Description = module.Description,
        };

    public static GrpcContracts.AudioItem MapToMessage(this AudioItem audioItem)
        => new GrpcContracts.AudioItem
        {
            AudioUrl = audioItem.AudioUrl,
            Transcription = audioItem.Transcription,
        };

    public static GrpcContracts.FlashCard MapToMessage(this FlashCard flashCard)
        => new GrpcContracts.FlashCard
        {
            Word = flashCard.Word,
            Translation = flashCard.Translation,
            ExampleSentence = flashCard.ExampleSentence,
            Synonyms = { flashCard.Synonyms },
        };

    public static AudioItem MapToModel(this GrpcContracts.AudioItem audioItem)
        => new AudioItem(audioItem.AudioUrl, audioItem.Transcription);

    public static FlashCard MapToModel(this GrpcContracts.FlashCard flashCard)
        => new FlashCard(flashCard.Word, flashCard.Translation, flashCard.ExampleSentence, flashCard.Synonyms);

    public static GrpcContracts.Lesson MapToMessage(this LessonDto lesson)
    {
        var grpcLesson = new GrpcContracts.Lesson
        {
            Id = lesson.Id,
            ModuleId = lesson.ModuleId,
            LessonType = lesson.LessonType,
            LessonNumber = lesson.LessonNumber,
            Name = lesson.Name,
            Description = lesson.Description,
        };

        switch (lesson.Content)
        {
            case TheoryLessonContent theoryLessonContent:
                grpcLesson.TheoryLessonContent = new GrpcContracts.TheoryLessonContent()
                    { Theory = theoryLessonContent.Theory };
                break;
            case VideoLessonContent videoLessonContent:
                grpcLesson.VideoLessonContent = new GrpcContracts.VideoLessonContent()
                    { VideoUrls = { videoLessonContent.VideoUrls, } };
                break;
            case AudioLessonContent audioLessonContent:
                grpcLesson.AudioLessonContent = new GrpcContracts.AudioLessonContent()
                    { AudioItem = { audioLessonContent.AudioItems.Select(a => a.MapToMessage()) } };
                break;
            case FlashCardLessonContent flashCardLessonContent:
                grpcLesson.FlashCardLessonContent = new GrpcContracts.FlashCardLessonContent()
                    { FlashCards = { flashCardLessonContent.FlashCards.Select(f => f.MapToMessage()) } };
                break;
            default:
                throw new Exception("Unknown content type");
        }

        return grpcLesson;
    }

    public static LessonContent MapContent(this AddLessonRequest addLessonRequest)
    {
        return addLessonRequest.ContentCase switch
        {
            AddLessonRequest.ContentOneofCase.None => new LessonContent(),
            AddLessonRequest.ContentOneofCase.TheoryLessonContent => new TheoryLessonContent(addLessonRequest.TheoryLessonContent.Theory),
            AddLessonRequest.ContentOneofCase.VideoLessonContent => new VideoLessonContent(addLessonRequest.VideoLessonContent.VideoUrls),
            AddLessonRequest.ContentOneofCase.AudioLessonContent => new AudioLessonContent(addLessonRequest.AudioLessonContent.AudioItem.Select(a => a.MapToModel()).ToList()),
            AddLessonRequest.ContentOneofCase.FlashCardLessonContent => new FlashCardLessonContent(addLessonRequest.FlashCardLessonContent.FlashCards.Select(f => f.MapToModel()).ToList()),
            _ => new LessonContent(),
        };
    }
}
