using ChocolateWorldApp.Api.Middleware;
using ChocolateWorldApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")));
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

var app = builder.Build();
app.UseMiddleware<ExcptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
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
    Log.Information("Starting web host - ChcolateWorldApp.Api");
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
