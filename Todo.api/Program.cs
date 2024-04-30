using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Todo.api.infrastructure.Auth;
using Todo.api.infrastructure.Exstensions;
using Todo.persistance;
using Todo.persistance.Context;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateLogger();

try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);

    // Full setup of serilog. We read log settings from appsettings.json
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    builder.Services.AddDbContext<TodoContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))));
    builder.Services.AddCustomServices(builder.Configuration);
    builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection(nameof(AuthConfig)));
    builder.Services.AddTokenAuth(builder.Configuration);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddConfiguredSwagger();
    builder.Services.AddCustomVersioning();

    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.AddCustomRequestResponseLogging();
    app.ConfigureMapster();

    app.AddGlobalErrorHandling();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(opts => {
            opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        }  );
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexcpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

return 0;
