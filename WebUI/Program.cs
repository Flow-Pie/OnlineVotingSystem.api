using WebUI.Components;
using Syncfusion.Blazor;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5256/";


builder.Services.AddHttpClient<IElectionsService, ElectionService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IUsersService, UsersService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

// Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Syncfusion Charts
builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Register Syncfusion License
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc2NTEwOUAzMjM4MmUzMDJlMzBMMmV5c0wrUmdFUFIyOEVScDJXcUExT005YjRMUlZEOHkzdkcwRjY4MzMwPQ==");

app.Run();