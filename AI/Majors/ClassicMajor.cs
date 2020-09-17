namespace CheckersAI.AI.Majors
{
    using Model;
    public class ClassicMajor : Major
    {
        public override double Predict(State state)
        {
            double whitepower = 0,
                   blackpower = 0;
            for (int i = 0; i < 32; i++)
            {
                switch (state[i])
                {
                    case Piece.White:
                        whitepower++;
                        break;
                    case Piece.Black:
                        blackpower++;
                        break;
                    case Piece.WhiteChecker:
                        whitepower += 3;
                        break;
                    case Piece.BlackChecker:
                        blackpower += 3;
                        break;
                }
            }
            return whitepower / (whitepower + blackpower);
        }

        public override double[] Predict(params State[] state)
        {
            double[] probs = new double[state.Length];
            for (int n = 0; n < probs.Length; n++)
                probs[n] = this.Predict(state[n]);
            return probs;
        }
    }
}