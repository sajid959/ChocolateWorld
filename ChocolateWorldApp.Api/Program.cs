using ChocolateWorldApp.Api.Middleware;
using ChocolateWorldApp.Application;
using ChocolateWorldApp.Infrastructure;
using Microsoft.OpenApi;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, service, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(service);
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "ChocolateWorldApp Api",
            Version = "v1",
            Description = "ChocolateWorldApp API Document",
            Contact = new OpenApiContact
            {
                Name = "ChocolateWorldApp Team",
                Email = "luckyansari1234@gmail.com"
            }
        }
    );
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        } 
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExcptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.DocumentTitle = "ChocolateWorldApp Api";
            options.DefaultModelsExpandDepth(-1);
            options.DisplayRequestDuration();    // how long each request took
        }
        );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
try
{
    Log.Information("Starting web host - ChocolateWorldApp.Api");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
//app.Run();
