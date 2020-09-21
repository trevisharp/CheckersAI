using System.Threading.Tasks;

namespace CheckersAI.Model
{
    public abstract class NonAsyncPlayer : Player
    {
        public abstract State ChoosePlay(State initial, bool aswhite = true);
        public override async Task<State> ChoosePlayAsync(State initial, bool aswhite = true)
            => await Task.Run(() => this.ChoosePlay(initial, aswhite));
    }
}