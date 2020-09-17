using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckersAI.Model
{
    public abstract class NonAsyncPlayer : Player
    {
        public override async Task<State> ChoosePlayAsync(IEnumerable<State> playlist, bool aswhite = true)
            => await Task.Run(() => this.ChoosePlay(playlist, aswhite));
    }
}