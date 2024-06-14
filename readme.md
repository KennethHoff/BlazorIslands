# Blazor Islands

This is a prototype for how to render Blazor Components with JavaScript.

The idea is to statically render most of the HTML on the page on the server, but occasionally you need "islands of interactivity".

This is where `Blazor Islands` comes in.

This is only needed for Blazor SSR (Server Side Rendering) as Blazor `InteractiveWebAssembly`/`InteractiveServer` already supports JavaScript interop through the [IJSRuntime](https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/?view=aspnetcore-8.0) interface.

## How it works

It works by adding a `JavaScriptSourceFeature` [Feature](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/request-features?view=aspnetcore-8.0) to the `HttpContext.Features` collection, which is then used by the `JavaScriptSourceMiddleware` to append `<script>` tags to the end of the HTML document (... after the `<html>` tag :> )