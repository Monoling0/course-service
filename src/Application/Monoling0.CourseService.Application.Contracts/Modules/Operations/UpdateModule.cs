namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class UpdateModule
{
    public readonly record struct Request(long ModuleId, string? Name, string? Description);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success() : Response;

        public sealed record ModuleNotFound : Response;
    }
}