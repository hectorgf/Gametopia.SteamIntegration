using Gametopia.Contracts.SteamIntegration;
using Gametopia.Contracts.SteamIntegration.SteamGamesService;
using System.Text.Json;

public class GameService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private string _apiKey;

    public GameService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;

        _apiKey = _configuration["Steam:ApiKey"];
    }

    public async Task<List<GameDto>> GetUserGamesAsync(string steamId)
    {
        var client = _httpClientFactory.CreateClient("SteamAPI");
        var response = await client.GetAsync($"IPlayerService/GetOwnedGames/v0001/?key={_apiKey}&steamid={steamId}&format=json");

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<SteamGamesResponse>(jsonResponse);

        return data?.Games ?? new List<GameDto>();
    }
}