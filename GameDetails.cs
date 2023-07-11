using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EquisoftWorkSample
{
    public class GameDetails
    {
        public int Player1Wins { get; set; }

        public int Player2Wins { get; set; }

        public int? Winner { get; set; }

        public string Player1LastMove { get; set; }

        public string Player2LastMove { get; set; }

        public string State { get; set; }
    }
}
