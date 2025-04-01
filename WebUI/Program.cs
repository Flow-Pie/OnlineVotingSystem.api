using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WebUI.Components;
using Syncfusion.Blazor;
using WebUI.Services;


var builder = WebApplication.CreateBuilder(args);

// Ensure API base URL is correctly formatted
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]?.TrimEnd('/') + "/" ?? "http://localhost:5256";

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod() 
            .AllowAnyHeader();
    });
});

// Register services as Scoped
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IElectionsService, ElectionService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IPositionsService, PositionsService>();
builder.Services.AddScoped<ICandidatesService, CandidatesService>();
builder.Services.AddScoped<IVoteService, VoteService>();

// Configure HttpClient instances
builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IElectionsService, ElectionService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IUsersService, UsersService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IPositionsService, PositionsService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<ICandidatesService, CandidatesService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IVoteService, VoteService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

// Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Syncfusion Charts
builder.Services.AddSyncfusionBlazor();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>(); 

builder.Logging.SetMinimumLevel(LogLevel.Debug);  // Log everything from Debug level
builder.Logging.AddConsole();

var app = builder.Build();

// Ensure routing before CORS
app.UseRouting();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); 
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAntiforgery();

// Blazor server setup
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Register Syncfusion License
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc3NTY2NkAzMjM4MmUzMDJlMzBMMmV5c0wrUmdFUFIyOEVScDJXcUExT005YjRMUlZEOHkzdkcwRjY4MzMwPQ==");

app.Run();