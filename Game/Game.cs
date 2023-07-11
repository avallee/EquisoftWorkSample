namespace EquisoftWorkSample.Game
{
    public class Game
    {
        public DateTime Created { get; init; }

        public Rules CurrentRules { get; init; }

        public int WinsRequired { get; init; }

        public class PlayerState
        {
            public int Wins { get; set; } = 0;

            public int PlayerNumer { get; init; }

            public Moves Move { get; set; } = Moves.None;

            public Moves LastMove { get; set; } = Moves.None;

        }

        // Running out of time, this should be enum
        public string GameState { get
            {
                if (!MovesExtensions.Playable.Contains(Player1.Move) && !MovesExtensions.Playable.Contains(Player2.Move))
                {
                    return "none";
                }
                if (MovesExtensions.Playable.Contains(Player1.Move))
                {
                    return "1";
                }
                if (MovesExtensions.Playable.Contains(Player2.Move))
                {
                    return "2";
                }
                return "both";
            } }

        public PlayerState Player1 { get; set; }

        public PlayerState Player2 { get; set; }

        public bool IsPlayableMovesSelected {  get
            {
                return MovesExtensions.Playable.Contains(Player1.Move) &&
                    MovesExtensions.Playable.Contains(Player2.Move);
            } }

        private PlayerState? WinningPlayer { get
            {
                if (Player1.Wins >= WinsRequired) return Player1;
                if (Player2.Wins >= WinsRequired) return Player2;
                return null;
            } }

        public int? WinningPlayerNumber => WinningPlayer?.PlayerNumer;

        public Game()
        {
            Created = DateTime.Now;
            CurrentRules = Rules.Default;
            WinsRequired = 3;
            Player1 = new PlayerState { PlayerNumer = 1};
            Player2 = new PlayerState { PlayerNumer = 2};
        }

        public void Player1Move(Moves move)
        {
            Player1.Move = move;
        }

        public void Player2Move(Moves move) {
            Player2.Move = move;
        }

        private void ResetMoves()
        {
            Player1.LastMove = Player1.Move;
            Player2.LastMove = Player2.Move;
            Player1.Move = Moves.None;
            Player2.Move = Moves.None;
        }

        // Returns true if game has a winner
        public bool Play()
        {
            if (!IsPlayableMovesSelected) return false;

            var result = CurrentRules.Check(Player1.Move, Player2.Move);
            ResetMoves();

            if (result == Result.Player1)
            {
                Player1.Wins += 1;
                return true;
            }
            if (result == Result.Player2)
            {
                Player2.Wins += 1;
                return true;
            }

            return false;
        }
    }
}
