namespace EquisoftWorkSample.Game
{
    public enum Result
    {
        Incomplete,
        Draw,
        Player1,
        Player2
    }

    public class Rules
    {
        public static Rules Default { get; } = BuildDefaultRules();

        private readonly Dictionary<Tuple<Moves, Moves>, Result> _rules;

        public Rules(Dictionary<Tuple<Moves, Moves>, Result> rules)
        {
            _rules = rules;
        }

        public Result Check(Moves player1, Moves player2)
        {
            if (player1 == Moves.None || player2 == Moves.None)
            {
                return Result.Incomplete;
            }
            if (player1 == player2)
            {
                return Result.Draw;
            }

            if (_rules.TryGetValue(Tuple.Create(player1, player2), out var result))
            {
                return result;
            }
            return Result.Draw;
        }

        public IEnumerable<Moves> BeatenBy(Moves player1)
        {
            return MovesExtensions.Playable.Where(p2 => Check(player1, p2) == Result.Player2);
        }

        public static Rules BuildDefaultRules()
        {
            // This is a simplified rules list as would be shown in the specs document
            var beats = new List<Tuple<Moves, Moves>>
            {
                Tuple.Create(Moves.Rock, Moves.Scissors),
                Tuple.Create(Moves.Scissors, Moves.Paper),
                Tuple.Create(Moves.Paper, Moves.Rock)
            };


            // Fill out the rules
            var rules = new Dictionary<Tuple<Moves, Moves>, Result>();
            beats.ForEach(player1wins =>
            {
                rules.Add(player1wins, Result.Player1);
                var player2wins = Tuple.Create(player1wins.Item2, player1wins.Item1);
                rules.Add(player2wins, Result.Player2);
            });

            // Future growth: Add validation here if rules were loaded externally.
            // Design question when expanding: If a move pairing does not have a defined winner
            // should it be considered a draw (currently implemented above) or signal an error.

            return new Rules(rules);
        }
    }
}
