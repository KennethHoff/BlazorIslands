using BlazorIslands;
using BlazorIslands.Components;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddRazorComponents();
builder.Services.AddBlazorIslands();

var app = builder.Build();
app.UseBlazorIslands();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
