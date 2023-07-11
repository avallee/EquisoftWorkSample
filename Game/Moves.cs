namespace EquisoftWorkSample.Game
{
    public enum Moves
    {
        None = 0,
        Rock,
        Paper,
        Scissors
    }

    public static class MovesExtensions
    {
        public static IEnumerable<Moves> Playable { get; } = Enum.GetValues(typeof(Moves))
            .Cast<Moves>().Where(x => x != Moves.None );
    }
}
