using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckersAI.Model
{
    public abstract class Player
    {
        public abstract Task<State> ChoosePlayAsync(State initial, bool aswhite = true);
    }
}