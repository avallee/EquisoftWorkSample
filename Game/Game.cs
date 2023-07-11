namespace EquisoftWorkSample.Game
{
    public class Game
    {
        public Rules CurrentRules { get; init; }

        public int WinsRequired { get; init; }

        private class PlayerState
        {
            public int Wins { get; set; } = 0;

            public int PlayerNumer { get; init; }

            public Moves Move { get; set; } = Moves.None;

        }

        private PlayerState Player1 { get; set; }

        private PlayerState Player2 { get; set; }

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

        private int? WinningPlayerNumber => WinningPlayer?.PlayerNumer;

        public Game()
        {
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
