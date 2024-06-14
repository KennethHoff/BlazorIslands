# Blazor Islands

This is a prototype for how to render Blazor Components with JavaScript.

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
    1. This can be done in three main ways:
        1. By injecting the `HttpContext` (or `IHttpContextAccessor`), retrieving the `IJavaScriptSourceFeature` and
           adding sources to it, by calling `context.Features.Get<IJavaScriptSourceFeature>().AddSource(...)`.
        2. By injecting the `IJavaScriptSourceFeature`, and then calling `AddSource(...)` on it.
        3. By creating a Razor Component that inherits from `JavaScriptComponentBase` and overriding the `JavaScriptSources` property.