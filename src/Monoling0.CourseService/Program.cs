// See https://aka.ms/new-console-template for more information

using Monoling0.CourseService.Application.Extensions;
using Monoling0.CourseService.Extensions;
using Monoling0.CourseService.Infrastructure.Persistence.Extensions;
using Monoling0.CourseService.Presentation.Grpc.Controllers;

// Console.WriteLine("Hello, World!");
// var collection = new ServiceCollection();
// collection.AddMigrations();
// ServiceProvider serviceProvider = collection.BuildServiceProvider();
// await serviceProvider.RunMigrations();
// Console.WriteLine("OK :)");
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddConfiguration(builder.Configuration);
builder.Services.ConfigureDatasource();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMigrations();

WebApplication app = builder.Build();

app.MapGrpcService<CourseController>();
app.MapGrpcService<ModuleController>();
app.MapGrpcService<LessonController>();
app.MapGrpcReflectionService();
app.Run("http://localhost:5005");
