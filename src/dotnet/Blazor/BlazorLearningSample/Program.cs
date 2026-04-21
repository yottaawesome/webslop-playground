// ───────────────────────────────────────────────────────────────────────────
// Program.cs — Application entry point for the Blazor Web App.
//
// A Blazor Web App (introduced in .NET 8, still the default in .NET 10) is
// really just an ASP.NET Core app that hosts Razor Components. The same
// components can be rendered:
//
//   • Statically on the server (SSR) — no interactivity, fastest first paint.
//   • Interactively on the server   — a SignalR "circuit" streams UI diffs
//                                     and input events between browser & server.
//   • Interactively in the browser  — via Blazor WebAssembly (not enabled in
//                                     this sample; see README for how).
//   • Auto                          — server first, then silently hands off
//                                     to WebAssembly once it's downloaded.
//
// This sample uses the *Server* interactive render mode, which is the
// simplest to reason about while learning.
// ───────────────────────────────────────────────────────────────────────────

using BlazorLearningSample.Components;
using BlazorLearningSample.Services;

var builder = WebApplication.CreateBuilder(args);

// ── 1. Register Razor Components + the Interactive Server render mode.
//        Without AddInteractiveServerComponents() the app would only do
//        static server rendering — buttons wouldn't do anything.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ── 2. Dependency Injection registrations.
//
//        Scoped → one instance per user circuit (Blazor Server) OR per
//                 HTTP request (classic ASP.NET). In practice that means
//                 "one per browser tab" for Blazor Server — perfect for
//                 per-user state like a shopping cart.
//        Transient → a new instance every time it's resolved.
//        Singleton → one instance shared across the whole application
//                    (i.e. across ALL users in a Blazor Server app —
//                    usually NOT what you want for user data!).
builder.Services.AddScoped<AppState>();
builder.Services.AddTransient<IQuoteService, QuoteService>();

// An HttpClient so demo components can call external APIs. In a real app
// prefer IHttpClientFactory (AddHttpClient<T>()) for connection pooling and
// named/typed clients.
builder.Services.AddHttpClient();

var app = builder.Build();

// ── 3. Standard ASP.NET Core middleware pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// Anti-forgery middleware is required by Blazor's interactive forms.
app.UseAntiforgery();

// Maps static assets (CSS/JS/images) with fingerprinted URLs for caching.
app.MapStaticAssets();

// ── 4. Map the root <App /> component and enable interactive server.
//        This is the "root" of the Blazor component tree; everything else
//        is reached through <Router /> inside App.razor → Routes.razor.
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
