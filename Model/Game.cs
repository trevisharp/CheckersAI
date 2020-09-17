using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckersAI.Model
{
    public enum PlayResult
    {
        Nothing = 0,
        WhiteWin = 1,
        BlackWin = 2,
        Draw = 4
    }

    public class Game
    {
        public Player WhitePlayer { get; set; }
        public Player BlackPlayer { get; set; }

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

        public PlayResult Play()
        {
            if (WhiteTime)
                this.State = WhitePlayer.ChoosePlay(State.Next(true), true);
            else this.State = BlackPlayer.ChoosePlay(State.Next(false), false);
            WhiteTime = !WhiteTime;

            //Test Win/Lose/Draw conditions
            return PlayResult.Nothing;
        }

        public async Task<PlayResult> PlayAsync()
        {
            if (WhiteTime)
                this.State = await WhitePlayer.ChoosePlayAsync(State.Next(true), true);
            else this.State = await BlackPlayer.ChoosePlayAsync(State.Next(false), false);
            WhiteTime = !WhiteTime;

            //Test Win/Lose/Draw conditions
            return PlayResult.Nothing;
        }

        public bool TryMove(int origin, int target)
        {
            var newstate = State.Copy();
            newstate[target] = newstate[origin];
            newstate[origin] = Piece.Empty;
            var next = State.Next(WhiteTime);
            foreach (var state in next)
            {
                if (state[origin] == Piece.Empty &&
                    state[target] != Piece.Empty)
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