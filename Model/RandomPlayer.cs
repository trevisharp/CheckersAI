using System.Linq;
using System;

namespace CheckersAI.Model
{
    public class RandomPlayer : NonAsyncPlayer
    {
        private Random rand = new Random(DateTime.Now.Millisecond);

        public override State ChoosePlay(State initial, bool aswhite = true)
        {
            var array = initial.Next().ToArray();
            return array[rand.Next() % array.Length];
        }
    }
}