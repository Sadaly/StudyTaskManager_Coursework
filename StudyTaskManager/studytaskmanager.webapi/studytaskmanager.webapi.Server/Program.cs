using StudyTaskManager.Application;
using StudyTaskManager.Infrastructure;
using StudyTaskManager.Persistence;
using Serilog;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Gatherly.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StudyTaskManager.Persistence.Interceptors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
                StudyTaskManager.Infrastructure.AssemblyReference.Assembly,
                StudyTaskManager.Persistence.AssemblyReference.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(StudyTaskManager.Application.AssemblyReference.Assembly));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(StudyTaskManager.Application.AssemblyReference.Assembly,
    includeInternalTypes: true);

string? connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>(
    (sp, optionsBuilder) =>
    {
        var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

        if (interceptor != null)
            optionsBuilder.UseSqlServer(connectionString)
                .AddInterceptors(interceptor);
    });
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
