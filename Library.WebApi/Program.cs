using Library.Infrastructure.EntityFramework;
using Library.WebApi.Extensions;
using Library.WebApi.Middlewares;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json");
        
        builder.Services.AddValidators();
        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddPolicies();
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddDbContext<DataContext>();
        builder.Services.AddScopedServices();
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}