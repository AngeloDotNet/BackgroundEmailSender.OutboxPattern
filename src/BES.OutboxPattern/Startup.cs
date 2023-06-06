using BES.OutboxPattern.BusinessLayer.Extensions;
using BES.OutboxPattern.DataAccessLayer;
using CustomLibrary.EFCore.Extensions;
using NET6CustomLibrary.Swagger;

namespace BES.OutboxPattern;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy("BES.OutboxPattern", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");

        //Creazione migration: Add-Migration InitialMigration
        //Esecuzione migration: Update-Database
        services.AddDbContextForSQLServer<AppDbContext>(connectionString, 3, string.Empty);
        services.AddDbContextServicesGenerics<AppDbContext>();

        services.AddSwaggerGenConfig("BES.OutboxPattern", "v1", string.Empty);
        services.AddServicesRegistration(Configuration, connectionString);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.UseCors("BES.OutboxPattern");
        app.AddUseSwaggerUI("BES OutboxPattern v1");

        app.UseRouting();
        app.UseHealthChecksUI();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}