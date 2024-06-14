# Blazor Islands

This is a proof-of-concept for how to render Blazor Components with JavaScript.

The idea is to statically render most of the HTML on the page on the server, but occasionally you need "islands of
interactivity".

This is where `Blazor Islands` comes in.

This is only needed for Blazor SSR (Server Side Rendering) as Blazor `InteractiveWebAssembly`/`InteractiveServer`
already supports JavaScript interop through
the [IJSRuntime](https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/?view=aspnetcore-8.0)
interface.

## How it works

It works by adding
a `JavaScriptSourceFeature` [Feature](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/request-features?view=aspnetcore-8.0)
to the `HttpContext.Features` collection, which is then used by the `JavaScriptSourceMiddleware` to append `<script>`
tags to the end of the HTML document (... after the `<html>` tag :> )

## How to use

1. Add the various `BlazorIslands` services to your `IServiceCollection`.
    1. Call `builder.Services.AddBlazorIslands();` in your `Program.cs` file.
2. Add the `BlazorIslands` middleware to your `IApplicationBuilder`.
    1. Call `app.UseBlazorIslands();` in your `Program.cs` file.
3. Add JavaScript sources to the `IJavaScriptSourceFeature` feature.
    1. This can be done in two ways:
        1. By creating a Razor Component that inherits from `JavaScriptComponentBase` and overriding
           the `JavaScriptSources` property.
            * This is the most idiomatic way to use `BlazorIslands`.
        2. By accessing the `IJavaScriptSourceFeature` from the `HttpContext.Features` collection, and then calling
           `AddSource(...)` on it.
           * This is useful for when you need to add JavaScript sources from a service or other non-component class.