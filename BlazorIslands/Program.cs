using BlazorIslands.Components;
using BlazorIslands.Features;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddRazorComponents();

builder.Services.AddSingleton<JavaScriptSourceMiddleware>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IJavaScriptSourceFeature>(provider =>
{
    if (provider.GetRequiredService<IHttpContextAccessor>().HttpContext is not { } httpContext)
    {
        throw new NotSupportedException(
            "You must use the JavaScriptSourceFeature within an HTTP context."
        );
    }

    if (httpContext.Features.Get<IJavaScriptSourceFeature>() is { } feature)
    {
        return feature;
    }

    feature = new JavaScriptSourceFeature();
    httpContext.Features.Set(feature);
    return feature;
});

var app = builder.Build();
app.UseMiddleware<JavaScriptSourceMiddleware>();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
