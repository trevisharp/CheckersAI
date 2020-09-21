namespace CheckersAI.AI
{
    using Model;
    /// <summary>
    /// Main AI Model logic base class
    /// </summary>
    public abstract class Marshal
    {
        public abstract State Play(State initial, bool aswhite);
        public abstract void Reestart();
    }
}