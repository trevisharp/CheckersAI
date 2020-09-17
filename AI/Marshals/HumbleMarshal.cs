using System.Linq;

namespace CheckersAI.AI.Marshals
{
    using Model;
    public class HumbleMarshal : Marshal
    {
        public Major Major { get; set; }

        public override State Play(State[] states, bool aswhite)
        {
            double[] probs = this.Major.Predict(states);
            if (!aswhite)
                probs = probs.Select(p => 1 - p).ToArray();
            int index = 0;
            double max = probs[0];
            for (int i = 1; i < probs.Length; i++)
            {
                if (probs[i] > max)
                {
                    max = probs[i];
                    index = i;
                }
            }
            return states[index];
        }

        public override void Reestart() { }
    }
}