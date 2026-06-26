using CashFlow202.Web.Client.Models;

namespace CashFlow202.Web.Client.Services;

/// <summary>
/// Central service that holds the game state and notifies subscribers on changes.
/// </summary>
public class GameStateService(LocalStorageService storage)
{
    public GameState State { get; private set; } = new();
    public event Action? OnChange;

    public async Task LoadAsync()
    {
        var saved = await storage.LoadAsync<GameState>();
        if (saved is not null)
        {
            State = saved;
            NotifyChanged();
        }
    }

    public async Task SaveAsync()
    {
        await storage.SaveAsync(State);
        NotifyChanged();
    }

    public async Task ResetAsync()
    {
        State = new GameState();
        await storage.ClearAsync();
        NotifyChanged();
    }

    /// <summary>Apply a mutation and auto-save.</summary>
    public async Task UpdateAsync(Action<GameState> mutate)
    {
        mutate(State);
        await SaveAsync();
    }

    private void NotifyChanged() => OnChange?.Invoke();
}
