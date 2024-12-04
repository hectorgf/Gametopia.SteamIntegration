using Microsoft.AspNetCore.Mvc;

namespace Gametopia.SteamIntegration.Controllers
{
    [ApiController]
    [Route("api/steam")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("user-games/{steamId}")]
        public async Task<IActionResult> GetUserGames(string steamId)
        {
            var games = await _gameService.GetUserGamesAsync(steamId);
            return Ok(games);
        }
    }
}