using EquisoftWorkSample.Game;
using Microsoft.AspNetCore.Mvc;

namespace EquisoftWorkSample.Controllers
{
    [ApiController]
    [Route("/api/game/{id}")]
    public class GameController
    {
        private readonly IDictionary<Guid, Game.Game> _games;

        public GameController(IDictionary<Guid, Game.Game> games)
        {
            _games = games;
        }

        [HttpGet]
        public GameDetails? Get(Guid id)
        {
            if(_games.TryGetValue(id, out var game))
            {
                return new GameDetails
                {
                    State = game.GameState,
                    Winner = game.WinningPlayerNumber,
                    Player1LastMove = game.Player1.LastMove.ToString(),
                    Player2LastMove = game.Player2.LastMove.ToString(),
                    Player1Wins = game.Player1.Wins,
                    Player2Wins = game.Player2.Wins
                };
            }
            return null;
        }

        [HttpPost]
        public IActionResult Play(Guid id, [FromBody] GameMove movedata)
        {
            if (_games.TryGetValue(id, out var game))
            {
                var move = Enum.Parse<Moves>(movedata.Move);
                if (movedata.Player == 1)
                {
                    game.Player1Move(move);
                    if (movedata.Computer)
                    {
                        game.Player2Move(Moves.Rock);
                        var rnd = new Random();

                        var moveset = game.Player2.LastMove == Moves.None ? MovesExtensions.Playable : game.CurrentRules.BeatenBy(game.Player2.LastMove);

                        var computerMove = moveset.OrderBy(_ => rnd.Next()).First();
                        game.Player2Move(computerMove);
                    }
                }

                if(movedata.Player == 2)
                {
                    game.Player2Move(move);
                }

                if(game.IsPlayableMovesSelected)
                {
                    game.Play();
                }

                return new NoContentResult();
            }
            return new NotFoundResult();
        }
    }
}
