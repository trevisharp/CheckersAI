using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckersAI.Model
{
    public class HumanPlayer : Player
    {
        public override State ChoosePlay(IEnumerable<State> playlist, bool aswhite = true)
        {
            throw new System.NotImplementedException();
        }

        public override Task<State> ChoosePlayAsync(IEnumerable<State> playlist, bool aswhite = true)
        {
            throw new System.NotImplementedException();
        }
    }
}