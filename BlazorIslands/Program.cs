using BlazorIslands.Components;
using BlazorIslands.Features;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddRazorComponents();

builder.Services.AddSingleton<JavaScriptSourceMiddleware>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseMiddleware<JavaScriptSourceMiddleware>();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
