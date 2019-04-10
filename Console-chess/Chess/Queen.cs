using Board;

namespace Chess
{
    class Queen : Piece
    {
        public Queen(GameBoard board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}