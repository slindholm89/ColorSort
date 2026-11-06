using ColorSort.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace ColorSort.Services {
    public class StorageService {
        private readonly IJSRuntime _js;

        private const string Key = "colorsort-save";

        public StorageService(IJSRuntime js) {
            _js = js;
        }

        public async Task SaveGameAsync(GameState state) {
            var json =
                JsonSerializer.Serialize(state);

            await _js.InvokeVoidAsync(
                "localStorage.setItem",
                Key,
                json);
        }

        public async Task<GameState?> LoadGameAsync() {
            var json =
                await _js.InvokeAsync<string>(
                    "localStorage.getItem",
                    Key);

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonSerializer.Deserialize<GameState>(json);
        }

        public async Task ClearAsync() {
            await _js.InvokeVoidAsync(
                "localStorage.removeItem",
                Key);
        }
    }
}
