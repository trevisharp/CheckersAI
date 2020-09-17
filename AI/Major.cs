namespace CheckersAI.AI
{
    using Model;
    /// <summary>
    /// Avaliator of State
    /// </summary>
    public abstract class Major
    {
        public abstract double Predict(State state);
        public abstract double[] Predict(params State[] state);
    }
}