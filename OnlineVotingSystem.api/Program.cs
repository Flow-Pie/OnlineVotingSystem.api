// Imports

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.Endpoints;
using OnlineVotingSystem.api.Services;

var builder = WebApplication.CreateBuilder(args);

var connString =
    builder.Configuration.GetConnectionString("OnlineVotingSystem"); // Connection string from appsettings.json
builder.Services
    .AddSqlite<
        OnlineVotingSystemContext>(connString); // Add SQLite database context from OnlineVotingSystemContext.cs file

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
builder.Services.AddAuthorization(); // Add authorization services
builder.Services.AddScoped<JwtService>(); // Add JWT service for generating and validating JWT tokens

// Add authorization policy for admin-only access
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireClaim("IsAdmin", "true"));

// Add swagger services
builder.Services.AddEndpointsApiExplorer();

// Configure swagger
builder.Services.AddSwaggerGen(options =>
{
    // Add a security definition for JWT
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Online Voting System API",
        Version = "v1",
        Description = "API for managing elections, candidates, and votes."
    });

    // Add a security definition for JWT to the swagger document
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {your JWT token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Add a security requirement for JWT to the swagger document
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

// Create swagger endpoint for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapUsersEndpoints(); // Map users endpoints
app.MapElectionEndpoints(); // Map elections endpoints
app.MapPositionEndpoints(); // Map positions endpoints

app.MapAuthEndpoints(); // Map authentication endpoints

// Add authentication and authorization middleware for JWT
app.UseAuthentication();
app.UseAuthorization();

await app.MigrateDbAsync(); // Migrate the database to the latest version on app startup

app.Run(); // Run the application