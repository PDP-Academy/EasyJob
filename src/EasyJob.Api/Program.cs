using EasyJob.Api.Extensions;
using EasyJob.Api.Middlewares;

namespace EasyJob.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddDbContexts(builder.Configuration)
            .AddInfrastructure()
            .AddApplication();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}