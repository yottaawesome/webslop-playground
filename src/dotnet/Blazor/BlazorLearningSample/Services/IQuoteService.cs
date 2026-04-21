namespace BlazorLearningSample.Services;

/// <summary>
/// Simple service interface used to demonstrate Blazor's Dependency Injection.
/// Components consume it using the <c>@inject</c> directive, e.g.
/// <c>@inject IQuoteService Quotes</c>.
/// </summary>
public interface IQuoteService
{
    Task<string> GetRandomQuoteAsync();
}

/// <summary>
/// Default implementation. In a real app this might call an HTTP API or a DB.
/// </summary>
public sealed class QuoteService : IQuoteService
{
    private static readonly string[] Quotes =
    [
        "Premature optimization is the root of all evil. — Knuth",
        "Make it work, make it right, make it fast. — Kent Beck",
        "Programs must be written for people to read. — Abelson & Sussman",
        "Simplicity is prerequisite for reliability. — Dijkstra",
        "There are only two hard things in Computer Science: cache invalidation and naming things. — Phil Karlton"
    ];

    public async Task<string> GetRandomQuoteAsync()
    {
        // Simulate async I/O (DB / HTTP / file).
        await Task.Delay(200);
        return Quotes[Random.Shared.Next(Quotes.Length)];
    }
}
