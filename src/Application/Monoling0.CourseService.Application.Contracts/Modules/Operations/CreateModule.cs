using Monoling0.CourseService.Application.Contracts.Modules.Models;

namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class CreateModule
{
    public readonly record struct Request(long CourseId, string Name, string Description);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(ModuleDto Module) : Response;

        public sealed record CourseNotFound : Response;
    }
}