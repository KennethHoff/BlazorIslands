using Microsoft.AspNetCore.Builder;

namespace BlazorIslands;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseBlazorIslands(this IApplicationBuilder app)
    {
        app.UseMiddleware<BlazorIslandsMiddleware>();
        return app;
    }
}
