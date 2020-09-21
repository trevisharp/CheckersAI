using System.Linq;

namespace CheckersAI.Model
{
    using AI;

    public class AIPlayer : NonAsyncPlayer
    {
        public Marshal Marshal { get; set; }

        public override State ChoosePlay(State initial, bool aswhite = true)
            => Marshal.Play(initial, aswhite);
        
        public void Reestart() => this.Marshal.Reestart();
    }
}