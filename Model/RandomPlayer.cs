using System.Collections.Generic;
using System.Linq;
using System;

namespace CheckersAI.Model
{
    public class RandomPlayer : NonAsyncPlayer
    {
        private Random rand = new Random(DateTime.Now.Millisecond);

        public override State ChoosePlay(IEnumerable<State> playlist, bool aswhite = true)
        {
            var array = playlist.ToArray();
            return array[rand.Next() % array.Length];
        }
    }
}