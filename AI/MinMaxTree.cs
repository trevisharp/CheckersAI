using System.Linq;
using System;

namespace CheckersAI.AI
{
    using Model;

    public class MinMaxTree
    {
        public int Depth { get; private set; } = 1;
        public State RootState => NodeRoot.State;

        private Node NodeRoot { get; set; }
        private class Node
        {
            public Node(State value)
            {
                this.Children = null;
                this.State = value;
                this.Evaluation = null;
            }

            public Node[] Children { get; set; }
            public State State { get; set; }
            public double? Evaluation { get; set; }
        }

        public MinMaxTree(State root)
            => this.NodeRoot = new Node(root);

        private void expand(Node node, bool aswhite)
        {
            if (node.Children == null)
            {
                node.Children = node.State.Next(aswhite)
                    .Select(s => new Node(s))
                    .ToArray();
            }
            else
            {
                for (int n = 0; n < node.Children.Length; n++)
                    expand(node.Children[n], !aswhite);
            }
        }
        /// <summary>
        /// Expand tree in depth.
        /// </summary>
        /// <param name="depth">Number of layers of expansion.</param>
        /// <param name="aswhite">if the root expansion occurs with white pieces.</param>
        public void Expand(int depth, bool aswhite)
        {
            Depth += depth;
            while (depth-- > 0)
                expand(NodeRoot, aswhite);
        }

        private void evaluate(Node node, Func<State, double> evaluator, bool maxlevel)
        {
            if (node.Children == null)
            {
                node.Evaluation = evaluator(node.State);
            }
            else
            {
                double best = maxlevel ? double.MinValue : double.MaxValue;
                for (int n = 0; n < node.Children.Length; n++)
                {
                    evaluate(node.Children[n], evaluator, !maxlevel);
                    if (maxlevel && node.Children[n].Evaluation.Value > best)
                        best = node.Children[n].Evaluation.Value;
                    else if (!maxlevel && node.Children[n].Evaluation.Value < best)
                        best = node.Children[n].Evaluation.Value;
                }
                node.Evaluation = best;
            }
        }
        /// <summary>
        /// Evaluates the effectiveness of a state based on an evaluator
        /// using MinMax algorithm, analyzes in depth.
        /// </summary>
        /// <param name="evaluator">Evaluator used in analyze.</param>
        /// <param name="maxlevel">If the final evaluation should be the maximum value.</param>
        public void Evaluate(Func<State, double> evaluator, bool maxlevel)
        {
            evaluate(NodeRoot, evaluator, maxlevel);
        }

        /// <summary>
        /// If expanded and evaluated, choose the best node and set him as new root.
        /// </summary>
        /// <returns>Returns true if the choice was possible.</returns>
        public bool Choose()
        {
            if (NodeRoot.Children == null)
                return false;
            else if (!NodeRoot.Evaluation.HasValue)
                return false;
            for (int k = 0; k < NodeRoot.Children.Length; k++)
                if (NodeRoot.Evaluation == NodeRoot.Children[k].Evaluation)
                {
                    this.NodeRoot = NodeRoot.Children[k];
                    Depth--;
                    return true;
                }
            return false;
        }

        /// <summary>
        /// Choose a specifict state.
        /// </summary>
        /// <param name="state">The choosed state.</param>
        /// <returns>Returns true if the operation is successful.</returns>
        public bool Choose(State state)
        {
            if (NodeRoot.Children == null)
                return false;
            for (int k = 0; k < NodeRoot.Children.Length; k++)
                if (NodeRoot.Children[k].State == state)
                {
                    this.NodeRoot = NodeRoot.Children[k];
                    Depth--;
                    return true;
                }
            return false;
        }

        /// <summary>
        /// Expand tree, evaluate states and choose the best state.
        /// </summary>
        /// <param name="depth">Number of layers of expansion.</param>
        /// <param name="evaluator">Evaluator used in analyze.</param>
        /// <param name="ismax">If the final evaluation should be the maximum value.</param>
        /// <param name="aswhite">if the root expansion occurs with white pieces.</param>
        /// <returns>The picked state</returns>
        public State Pick(int depth, Func<State, double> evaluator, bool ismax, bool aswhite)
        {
            State picked = null;
            if (this.Depth < depth)
                Expand(depth - this.Depth, aswhite);
            Evaluate(evaluator, ismax);
            if (Choose())
            {
                picked = RootState;
                Expand(1, !aswhite);
            }
            return picked;
        }
    }
}