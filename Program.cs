using System.Text.Encodings.Web;
using DotNetEnv;
using FMAPI.Context;
using FMAPI.Service;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------

builder.Services.AddScoped<BbqService>();
builder.Services.AddHttpContextAccessor();

Env.Load();

// -------------------- ENV --------------------
string DB_HOST_BBQ = Environment.GetEnvironmentVariable("DB_HOST_BBQ")!;
string DB_PORT_BBQ = Environment.GetEnvironmentVariable("DB_PORT_BBQ")!;
string DB_NAME_BBQ = Environment.GetEnvironmentVariable("DB_NAME_BBQ")!;
string DB_USER_BBQ = Environment.GetEnvironmentVariable("DB_USER_BBQ")!;
string DB_PASSWORD_BBQ = Environment.GetEnvironmentVariable("DB_PASSWORD_BBQ")!;
// -------------------- CONNECTION STRINGS --------------------
string connString =
    $"Host={DB_HOST_BBQ};" +
    $"Port={DB_PORT_BBQ};" +
    $"Database={DB_NAME_BBQ};" +
    $"Username={DB_USER_BBQ};" +
    $"Password={DB_PASSWORD_BBQ};";


var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);

dataSourceBuilder.UseNetTopologySuite();
dataSourceBuilder.EnableDynamicJson();

var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<BbqContext>(options =>
    options.UseNpgsql(dataSource, o =>
    {
        o.UseNetTopologySuite();
    }));

// -------------------- CORS --------------------

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

// -------------------- AUTH --------------------


// -------------------- RATE LIMIT --------------------

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("api", config =>
    {
        config.PermitLimit = 60;
        config.Window = TimeSpan.FromMinutes(1);
        config.QueueLimit = 0;
    });
});

// -------------------- CONTROLLERS --------------------

builder.Services.AddControllers();

// -------------------- SWAGGER --------------------

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FMApi",
        Version = "v1"
    });
});

// -------------------- FORWARDED HEADERS --------------------

// -------------------- BUILD --------------------

var app = builder.Build();

// -------------------- PIPELINE --------------------

app.UseForwardedHeaders();

app.UseExceptionHandler("/Error");

app.UseHsts();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sta API v1");
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();