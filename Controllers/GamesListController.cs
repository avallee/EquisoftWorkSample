using EquisoftWorkSample.Game;
using Microsoft.AspNetCore.Mvc;

namespace EquisoftWorkSample.Controllers
{
    [ApiController]
    [Route("/api/games")]
    public class GamesListController
    {

        private readonly IDictionary<Guid, Game.Game> _games;

        public GamesListController(IDictionary<Guid, Game.Game> games)
        {
            _games = games;
        }

        [HttpGet]
        public IEnumerable<GameListItem> Get()
        {
            return _games
                .Select(g => new GameListItem() { Created = g.Value.Created, Id = g.Key, WinningPlayer = g.Value.WinningPlayerNumber })
                .OrderByDescending(game => game.Created);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var game = new Game.Game();
            var id = Guid.NewGuid();
            _games.Add(id, game);

            return new RedirectResult(String.Format("/api/game/{0}", id));
        }
    }
}
