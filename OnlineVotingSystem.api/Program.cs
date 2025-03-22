// Imports

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.Endpoints;
using OnlineVotingSystem.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Load connection string based on environment
var env = builder.Environment.EnvironmentName;
var connString = env == "Development"
    ? builder.Configuration.GetConnectionString("Development")
    : builder.Configuration.GetConnectionString("Production");

if (string.IsNullOrEmpty(connString))
{
    throw new InvalidOperationException("Database connection string is missing.");
}

// Configure Database (SQLite for Dev, PostgreSQL for Prod)
if (env == "Development")
{
    builder.Services.AddSqlite<OnlineVotingSystemContext>(connString);
}
else
{
    builder.Services.AddNpgsql<OnlineVotingSystemContext>(connString);
}

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtConfig"); // JWT settings from appsettings.json
    var key = Convert.FromBase64String(jwtSettings["Key"]!); // Convert the key from base64 string to byte array

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings["Issuer"],
        ValidateIssuer = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
    };
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtService>();

// Define authorization policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireClaim("IsAdmin", "true"));

// Enable CORS (for Swagger & frontend requests)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Online Voting System API",
        Version = "v1",
        Description = "API for managing elections, candidates, and votes."
    });

    // Configure Swagger to use JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {your JWT token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Voting System API v1");
        c.RoutePrefix = string.Empty; // Make Swagger available at root (`/`)
        c.DocumentTitle = "Online Voting System API";
    });
}

// Enable CORS
app.UseCors("AllowAll");

// Enable authentication & authorization
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapAuthEndpoints();
app.MapCandidateEndpoints();
app.MapElectionEndpoints();
app.MapPositionEndpoints();
app.MapUsersEndpoints();
app.MapVoteEndpoints();

// Apply pending migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OnlineVotingSystemContext>();
    dbContext.Database.EnsureCreated(); // Ensure DB exists
}

app.Run();
