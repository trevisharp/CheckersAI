using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckersAI.Model
{
    public abstract class Player
    {
        public abstract State ChoosePlay(IEnumerable<State> playlist, bool aswhite = true);
        public abstract Task<State> ChoosePlayAsync(IEnumerable<State> playlist, bool aswhite = true);
    }
}