using CDNPortalTutorial.Data;
using CDNPortalTutorial.Services.ServiceImplement;
using CDNPortalTutorial.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using CDNPortalTutorial.Middleware;
using FluentValidation;
using System.Reflection;
using CDNPortalTutorial.Behaviors;

// Create Logger Information
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("starting server.");

    // Create Builder
    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog
    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.WriteTo.Console();
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });

    // Add services to the container.
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Configure Security
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("CDNPortal"));
    });

    // Add Service
    builder.Services.AddScoped<UserService>();

    // Add Service with it's interface
    builder.Services.AddTransient<IUserService, UserService>();

    // Add Validator
    //builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();
    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    // Add MediatR
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Configure SeriLog
    app.UseSerilogRequestLogging();

    // Exception Handler
    app.UseExceptionHandler(options =>
    {
        options.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var exception = context.Features.Get<IExceptionHandlerFeature>();
            if (exception != null)
            {
                var message = $"{exception.Error.Message}";
                await context.Response.WriteAsync(message).ConfigureAwait(false);
            }
        });
    });

    // Use Middleware
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}