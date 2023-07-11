using Microsoft.AspNetCore.Mvc;

namespace EquisoftWorkSample.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesListController
    {
        public IEnumerable<GameListItem> Index()
        {
            return new List<GameListItem>
            {
                new GameListItem()
                {
                    Id = Guid.NewGuid(),
                    WinningPlayer = 1
                }
            };
        }
    }
}
