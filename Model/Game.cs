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

        /// <summary>
        /// Execute a single play.
        /// </summary>
        /// <returns></returns>
        public async Task<PlayResult> Play()
        {
            if (WhiteTime)
                this.State = await WhitePlayer.ChoosePlayAsync(State, true);
            else this.State = await BlackPlayer.ChoosePlayAsync(State, false);
            WhiteTime = !WhiteTime;

            //Test Win/Lose/Draw conditions
            return PlayResult.Nothing;
        }
    }
}