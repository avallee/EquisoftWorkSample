using EquisoftWorkSample.Game;
using Microsoft.AspNetCore.Mvc;

namespace EquisoftWorkSample.Controllers
{
    [ApiController]
    [Route("/api/moves")]
    public class MovesController
    {
        [HttpGet]
        public IEnumerable<string> Get() => MovesExtensions.Playable.Select(m=>m.ToString());
    }
}
