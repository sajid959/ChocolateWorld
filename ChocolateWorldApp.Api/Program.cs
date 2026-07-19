using System.Text;
using ChocolateWorldApp.Api.Middleware;
using ChocolateWorldApp.Application;
using ChocolateWorldApp.Infrastructure;
using ChocolateWorldApp.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your Jwt (without the 'Bearer' prefix"
            }
        );
        options.AddSecurityRequirement(document =>
            new OpenApiSecurityRequirement
            {
                [
                    new OpenApiSecuritySchemeReference("Bearer", document)
                ] = []
            });
        
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// creating an options for jwt from appsetting so that it can be used anywhere
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>() ?? throw new Exception("Missing jwt configuration");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                //validation of tokens
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                //fetching from appsettings
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                // fetching and converting to Binary
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                ClockSkew = TimeSpan.Zero
            };
        }
    );
builder.Services.AddAuthorization();

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
app.UseAuthentication();
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
