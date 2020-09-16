using System.Collections.Generic;

namespace CheckersIA.Model
{
    public class Game
    {
        public bool WhiteTime { get; set; } = true;
        public State State { get; set; }

        public Game()
        {
            State = new State();
            for (int j = 0; j < 12; j++)
                State[j] = Piece.White;
            for (int j = 20; j < 32; j++)
                State[j] = Piece.Black;
        }

        public bool TryMove(int origin, int target)
        {
            var newstate = State.Copy();
            newstate[target] = newstate[origin];
            newstate[origin] = Piece.Empty;
            var next = State.Next(WhiteTime);
            foreach (var state in next)
            {
                if (state[origin] == newstate[origin] &&
                    state[target] == newstate[target])
                {
                    this.State = state;
                    WhiteTime = !WhiteTime;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<State> ListMoves()
            => State.Next(WhiteTime);
    }
}