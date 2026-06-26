using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Text.Json;

namespace CashFlow202.Web.Client.Services;

/// <summary>
/// Wraps browser localStorage to persist game state across refreshes.
/// </summary>
public class LocalStorageService(IJSRuntime js, ILogger<LocalStorageService> logger)
{
    private const string Key = "cashflow202_state";
    private readonly JsonSerializerOptions _opts = new()
    {
        WriteIndented = false,
        PropertyNameCaseInsensitive = true,
    };

    public async Task SaveAsync<T>(T value)
    {
        var json = JsonSerializer.Serialize(value, _opts);
        await js.InvokeVoidAsync("localStorage.setItem", Key, json);
    }

    public async Task<T?> LoadAsync<T>()
    {
        try
        {
            var json = await js.InvokeAsync<string?>("localStorage.getItem", Key);
            if (string.IsNullOrEmpty(json)) return default;
            return JsonSerializer.Deserialize<T>(json, _opts);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Unable to load saved state from localStorage.");
            return default;
        }
    }

    public async Task ClearAsync()
    {
        await js.InvokeVoidAsync("localStorage.removeItem", Key);
    }
}
