using System.Threading.Tasks;
using System.Linq;
using System;

namespace CheckersAI.Model
{
    public class HumanPlayer : Player
    {
        private volatile int origin = -1;
        private volatile int tartget = -1;
        public int Origin
        {
            get => this.origin;
            set => this.origin = value;
        }
        public int Target
        {
            get => this.tartget;
            set => this.tartget = value;
        }

        public override async Task<State> ChoosePlayAsync(State initial, bool aswhite = true)
        {
            var playlist = initial.Next(aswhite).ToArray();
            State result;
            while (true)
            {
                result = initial.Copy();
                while (this.origin == -1 || this.tartget == -1)
                    await Task.Delay(200);
            
                result[this.tartget] = result[this.origin];
                result[this.origin] = Piece.Empty;

                for (int j = 0; j < playlist.Length; j++)
                {
                    if (playlist[j][this.tartget].IsAlly(result[this.tartget]) &&
                        (playlist[j][this.origin] == Piece.Empty || this.tartget == this.origin))
                    {
                        this.origin = this.tartget = -1;
                        if (this.OnPlay != null)
                            this.OnPlay(this, EventArgs.Empty);
                        return playlist[j];
                    }
                }
                this.origin = this.tartget = -1;
            }
        }

        public event EventHandler OnPlay;
    }
}