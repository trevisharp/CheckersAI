namespace CheckersAI.AI.Marshals
{
    using Model;
    public class DeepMarshal : Marshal
    {
        public int Depth { get; set; } = 5;

        public Major Major { get; set; }

        private MinMaxTree tree = null;

        public override State Play(State initial, bool aswhite)
        {
            if (tree == null)
                tree = new MinMaxTree(initial);
            else
            {
                if (!tree.Choose(initial))
                    tree = new MinMaxTree(initial);
            }
            return tree.Pick(this.Depth,
                s => this.Major.Predict(s),
                aswhite, aswhite);
        }

        public override void Reestart()
            => tree = null;
    }
}