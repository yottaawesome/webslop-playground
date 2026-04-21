namespace BlazorLearningSample.Services;

/// <summary>
/// A singleton "app state" service. Demonstrates how to share state between
/// unrelated components using Dependency Injection + an event callback.
///
/// Components subscribe to <see cref="OnChange"/> in OnInitialized and call
/// StateHasChanged() so they re-render when the state mutates.
///
/// NOTE: In a Blazor *Server* app, a true singleton is global across ALL
/// connected users. For per-user state, register as "scoped" instead — in
/// Blazor Server the DI scope lasts for the duration of a single circuit
/// (i.e. one user's SignalR connection), which is usually what you want.
/// This sample registers it Scoped (see Program.cs) so each browser tab has
/// its own counter.
/// </summary>
public sealed class AppState
{
    private int _globalCount;

    public int GlobalCount => _globalCount;

    // Classic .NET "observer" pattern: subscribers re-render when raised.
    public event Action? OnChange;

    public void Increment()
    {
        _globalCount++;
        NotifyStateChanged();
    }

    public void Reset()
    {
        _globalCount = 0;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
