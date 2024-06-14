using BlazorIslands;
using BlazorIslands.Components;
using BlazorIslands.Features;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddRazorComponents();
builder.Services.AddBlazorIslands();

var app = builder.Build();
app.UseBlazorIslands();

// Example of adding a JavaScript source to the rendered HTML, using middleware.
// (For some reason, this causes a lot of exceptions, but this is a proof of concept, so... ¯\_(ツ)_/¯)
app.Use(
    (context, next) =>
    {
        var jsSourceFeature = context.Features.Get<IJavaScriptSourceFeature>();
        jsSourceFeature?.AddSource(new JavaScriptSource("middleware.js"));
        return next();
    }
);

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
