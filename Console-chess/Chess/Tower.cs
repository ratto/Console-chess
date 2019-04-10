using Board;

namespace Chess
{
    class Tower : Piece
    {
        public Tower(GameBoard board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
