namespace CheckersAI.AI
{
    using Model;

    public abstract class Marshal
    {
        public abstract State Play(State[] states, bool aswhite);
        public abstract void Reestart();
    }
}