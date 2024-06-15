using BlazorIslands.Components;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddRazorComponents();

var app = builder.Build();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
