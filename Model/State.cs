using System;

namespace CheckersAI.Model
{
    public class State
    {
        /*
            ┌──┬──┬──┬──┬──┬──┬──┬──┐
            │  │28│  │29│  │30│  │31│ right line
            ├──┼──┼──┼──┼──┼──┼──┼──┤
            │24│  │25│  │26│  │27│  │ left line
            ├──┼──┼──┼──┼──┼──┼──┼──┤
            │  │20│  │21│  │22│  │23│ right line
            ├──┼──┼──┼──┼──┼──┼──┼──┤
            │16│  │17│  │18│  │19│  │ left line
            ├──┼──┼──┼──┼──┼──┼──┼──┤
            │  │12│  │13│  │14│  │15│ right line
            ├──┼──┼──┼──┼──┼──┼──┼──┤
            │08│  │09│  │10│  │11│  │ left line
            ├──┼──┼──┼──┼──┼──┼──┼──┤
            │  │04│  │05│  │06│  │07│ right line
            ├──┼──┼──┼──┼──┼──┼──┼──┤
            │00│  │01│  │02│  │03│  │ left line
            └──┴──┴──┴──┴──┴──┴──┴──┘
        */
        private Piece[] state = new Piece[32];

        public Piece this[int p]
        {
            get => state[p];
            set => state[p] = value;
        }

        public State Copy()
        {
            State newstate = new State();
            Array.Copy(state, newstate.state, 32);
            return newstate;
        }

        public override bool Equals(object obj)
            => obj is State s && this == s;

        public override int GetHashCode()
            => base.GetHashCode();

        public override string ToString()
            => base.ToString();

        public static bool operator ==(State s1, State s2)
        {
            for (int j = 0; j < 32; j++)
            {
                if (s1[j] != s2[j])
                    return false;
            }
            return true;
        }

        public static bool operator !=(State s1, State s2)
        {
            for (int j = 0; j < 32; j++)
            {
                if (s1[j] != s2[j])
                    return true;
            }
            return false;
        }
    }
}