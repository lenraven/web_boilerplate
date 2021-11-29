using Microsoft.Extensions.DependencyInjection.Extensions;

using Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.TryAddSingleton<ManifestRevFileVersionProvider>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
