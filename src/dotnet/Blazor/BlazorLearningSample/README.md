# Blazor Learning Sample

A small but thorough tour of **the core concepts of Blazor**, implemented as a
single [Blazor Web App](https://learn.microsoft.com/aspnet/core/blazor/) on
.NET 10. Every demo page is heavily commented — open the corresponding
`.razor` file alongside the running page to learn by example.

## What's demonstrated

| Page | URL | Concept |
| --- | --- | --- |
| `Home.razor` | `/` | Static server rendering (no JS, no circuit) |
| `Counter.razor` | `/counter` | Event handling + private state, `@rendermode InteractiveServer` |
| `ParametersDemo.razor` | `/parameters` | `[Parameter]`, `RenderFragment` / `ChildContent`, `EventCallback`, attribute splatting |
| `DataBindingDemo.razor` | `/binding` | One-way, two-way (`@bind`), `@bind:event`, `@bind:format`, `@bind:get`/`@bind:set` |
| `FormsDemo.razor` | `/forms` | `EditForm`, `DataAnnotationsValidator`, `InputText`/`InputNumber`/`InputSelect`/`InputCheckbox`/`InputTextArea`, `ValidationMessage` |
| `LifecycleDemo.razor` | `/lifecycle` | `OnInitialized`, `OnParametersSet`, `OnAfterRender`, `IDisposable`, thread-safe `InvokeAsync(StateHasChanged)` |
| `RoutingDemo.razor` | `/greet/{Name}/{Times:int}` | Multiple `@page` routes, route parameters, route constraints, `NavigationManager` |
| `DependencyInjectionDemo.razor` | `/di` | Custom services, `@inject`, framework services (`ILogger<T>`), service lifetimes |
| `CascadingDemo.razor` | `/cascading` | `CascadingValue` / `[CascadingParameter]` |
| `JsInteropDemo.razor` | `/jsinterop` | `IJSRuntime.InvokeVoidAsync` / `InvokeAsync<T>`, when it's safe to call JS |
| `StateDemo.razor` | `/state` | Sharing state between components through a DI-scoped service + event |
| `CodeBehindDemo.razor` (+ `.razor.cs`) | `/codebehind` | Splitting markup and logic with a `partial` class |
| `Weather.razor` | `/weather` | Async data, `[StreamRendering]` |

Supporting types:

- `Services/AppState.cs` — scoped service + change event
- `Services/IQuoteService.cs` — interface + implementation for DI demo
- `Models/ContactForm.cs` — `DataAnnotations`-decorated form model
- `Components/Shared/Alert.razor` — reusable alert with `ChildContent` + `EventCallback`
- `Components/Shared/UserCard.razor` — component with attribute splatting
- `Components/Shared/ThemedChild.razor` — consumer of a cascading parameter

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) (any newer major
  version usually works; this sample is pinned to `net10.0` in the csproj)
- Any modern browser

Verify with:

```powershell
dotnet --version
```

## Run it

From this directory:

```powershell
dotnet run
```

By default the launch profile binds to `http://localhost:5157`. Open that in
your browser and click through the menu on the left.

For a nicer inner dev loop use hot reload:

```powershell
dotnet watch
```

Edits to `.razor` / `.cs` files will be applied automatically, and in most
cases the browser will refresh itself.

## Project layout

```
BlazorLearningSample/
├── Program.cs                      ← App bootstrap, DI, middleware (heavily commented)
├── BlazorLearningSample.csproj     ← Targets net10.0
├── Models/
│   └── ContactForm.cs
├── Services/
│   ├── AppState.cs
│   └── IQuoteService.cs
├── wwwroot/                        ← Static assets (CSS, images)
└── Components/
    ├── App.razor                   ← Root host page (<html>)
    ├── Routes.razor                ← <Router>
    ├── _Imports.razor              ← @using shared across every .razor
    ├── Layout/
    │   ├── MainLayout.razor
    │   └── NavMenu.razor           ← Sidebar with a link per demo
    ├── Shared/
    │   ├── Alert.razor
    │   ├── UserCard.razor
    │   └── ThemedChild.razor
    └── Pages/                      ← One @page component per demo
        ├── Home.razor
        ├── Counter.razor
        ├── ParametersDemo.razor
        ├── DataBindingDemo.razor
        ├── FormsDemo.razor
        ├── LifecycleDemo.razor
        ├── RoutingDemo.razor
        ├── DependencyInjectionDemo.razor
        ├── CascadingDemo.razor
        ├── JsInteropDemo.razor
        ├── StateDemo.razor
        ├── CodeBehindDemo.razor (+ .razor.cs)
        ├── Weather.razor
        ├── Error.razor
        └── NotFound.razor
```

## Render modes — the one thing that trips everyone up

Blazor Web Apps support four render modes:

| Mode | Runs | When to use |
| --- | --- | --- |
| **Static Server** | On the server, no JS shipped | Read-only pages, SEO, fastest TTFB |
| **InteractiveServer** | Server, via a SignalR circuit | Fast startup, needs a persistent connection (used in this sample) |
| **InteractiveWebAssembly** | Browser (WASM) | Offline capability, no server roundtrip per event |
| **InteractiveAuto** | Server first → WebAssembly once downloaded | Best of both, more moving parts |

By default Razor Components are **statically** rendered. To opt a page into
interactivity, add `@rendermode InteractiveServer` at the top. You can see
this in `Counter.razor` (and in every demo that needs event handlers). The
`Home.razor` page deliberately has **no** rendermode so it ships as plain HTML.

To try WebAssembly instead, regenerate the project with:

```powershell
dotnet new blazor --interactivity WebAssembly
```

…or `--interactivity Auto` for both. You'll get an extra
`YourApp.Client` project that hosts the components running in WASM.

## Suggested reading order

1. `Program.cs` — understand the startup plumbing.
2. `Counter.razor` — smallest possible interactive component.
3. `ParametersDemo.razor` + `Shared/Alert.razor` — how components talk.
4. `DataBindingDemo.razor` — forms of `@bind`.
5. `LifecycleDemo.razor` — when code runs and why.
6. `RoutingDemo.razor` — `@page`, route params, `NavigationManager`.
7. `DependencyInjectionDemo.razor` + `Services/` — DI in practice.
8. `CascadingDemo.razor` — state down the tree.
9. `StateDemo.razor` — state across the tree.
10. `JsInteropDemo.razor` — escape hatch to JS.
11. `FormsDemo.razor` — put it all together with validation.
12. `CodeBehindDemo.razor` + `.razor.cs` — scaling up.

## Further reading

- [Blazor docs (Microsoft Learn)](https://learn.microsoft.com/aspnet/core/blazor/)
- [Render modes](https://learn.microsoft.com/aspnet/core/blazor/components/render-modes)
- [Lifecycle](https://learn.microsoft.com/aspnet/core/blazor/components/lifecycle)
- [Forms & validation](https://learn.microsoft.com/aspnet/core/blazor/forms/)
- [JS interop](https://learn.microsoft.com/aspnet/core/blazor/javascript-interoperability/)
