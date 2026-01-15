namespace Monoling0.CourseService.Application.Contracts.Modules.Operations;

public static class SwapModules
{
    public readonly record struct Request(long CourseId, int FirstModuleNumber, int SecondModuleNumber);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success() : Response;

        public sealed record ModuleNotFound : Response;
    }
}