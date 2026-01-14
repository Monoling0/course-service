// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Monoling0.CourseService.Infrastructure.Persistence.Extenstions;

Console.WriteLine("Hello, World!");
var collection = new ServiceCollection();
collection.AddMigrations();
ServiceProvider serviceProvider = collection.BuildServiceProvider();
await serviceProvider.RunMigrations();
Console.WriteLine("OK :)");
