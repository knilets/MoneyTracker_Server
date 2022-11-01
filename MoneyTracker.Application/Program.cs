using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Extensions;
using MoneyTracker.Application.Filters;

//const string CorsPolicyName = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add Controller Configuration
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpResponseExceptionFilter));
    options.CacheProfiles.Add("30SecondsCaching", new CacheProfile
    {
        Duration = 30
    });
});
builder.Services.AddResponseCaching();
builder.Services.AddHttpContextAccessor();

// Add Database Connections
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.ConfigureSqlContext(connectionString);

// Auto Mapper Configurations
builder.Services.ConfigureMapper();

// Add JWT Configuration
var jwtConfigurationSection = builder.Configuration.GetSection("JwtConfiguration");
builder.Services.AddSingleton<IConfigurationSection>(jwtConfigurationSection);

// Add Authentication Configuration
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(jwtConfigurationSection);

// Cors Policy
//builder.Services.AddCors(options =>
//{
//    var origins = builder.Configuration.GetValue<string>("JsCors")?
//                      .Split(',')
//                      .Where(origin => origin.StartsWith("http"))
//                      .ToArray()
//                  ?? (Array.Empty<string>());

//    options.AddPolicy(CorsPolicyName,
//        configurePolicy => configurePolicy.WithOrigins(origins)
//            .AllowCredentials()
//            .AllowAnyHeader()
//            .AllowAnyMethod());
//});

// Add Services Dependency Injections
builder.Services.ConfigureServiceDependencyInjection();

// Add HealthChecks
builder.Services.AddHealthChecks();

// Add Swagger Configuration.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseCors(CorsPolicyName);

app.UseHttpsRedirection();
app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health");

app.Run();