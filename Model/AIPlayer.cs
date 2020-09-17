using System.Collections.Generic;
using System.Linq;

namespace CheckersAI.Model
{
    using AI;

    public class AIPlayer : NonAsyncPlayer
    {
        public Marshal Marshal { get; set; }

        public override State ChoosePlay(IEnumerable<State> playlist, bool aswhite = true)
            => Marshal.Play(playlist.ToArray(), aswhite);
        
        public void Reestart() => this.Marshal.Reestart();
    }
}