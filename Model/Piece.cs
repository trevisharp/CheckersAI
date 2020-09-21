namespace CheckersAI.Model
{
    public enum Piece : byte
    {
        Empty = 0,
        White = 1,
        Black = 2,
        WhiteChecker = 3,
        BlackChecker = 4
    }

    public static class PieceExtension
    {
        public static bool IsAlly(this Piece p, Piece other)
        {
            return !(p == Piece.Empty || other == Piece.Empty ||
                (((byte)p % 2) == 0 ^ ((byte)other % 2) == 0));
        }
    }
}