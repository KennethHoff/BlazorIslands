using BlazorIslands;
using BlazorIslands.Components;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddRazorComponents();
builder.Services.AddBlazorIslands();

var app = builder.Build();
app.UseBlazorIslands();

// Example of adding a JavaScript source to the rendered HTML, using middleware.
app.Use(
    async (context, next) =>
    {
        await next();

        // If the feature is not available, create a new instance and add it to the context
        if (context.Features.Get<IBlazorIslandsFeature>() is not { } blazorIslandsFeature)
        {
            blazorIslandsFeature = new BlazorIslandsFeature();
            context.Features.Set(blazorIslandsFeature);
        }

        blazorIslandsFeature.AddSource(new ExternalJavaScriptSource("middleware.js"));
    }
);

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
