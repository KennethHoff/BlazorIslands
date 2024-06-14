using BlazorIslands;
using BlazorIslands.Components;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddRazorComponents();
builder.Services.AddBlazorIslands();

var app = builder.Build();
app.UseBlazorIslands();

// Example of adding a JavaScript source to the rendered HTML, using middleware.
app.Use(
    (context, next) =>
    {
        var jsSourceFeature = context.Features.Get<IJavaScriptSourceFeature>();
        jsSourceFeature?.AddSource(new ExternalJavaScriptSource("middleware.js"));
        return next();
    }
);

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
