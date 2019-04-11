using Board;

namespace Chess
{
    class King : Piece
    {
        public King(GameBoard board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] possibleMove()
        {
            bool[,] mat = new bool[gameBoard.Lines, gameBoard.Columns];

            Position pos = new Position(0, 0);

            //above
            pos.definePosition(position.Line - 1, position.Column);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            // right + above
            pos.definePosition(position.Line - 1, position.Column + 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            // right
            pos.definePosition(position.Line, position.Column + 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            // right + below
            pos.definePosition(position.Line + 1, position.Column + 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            // below
            pos.definePosition(position.Line + 1, position.Column);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            // left + above
            pos.definePosition(position.Line - 1, position.Column - 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            // left
            pos.definePosition(position.Line, position.Column - 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            // left + below
            pos.definePosition(position.Line + 1, position.Column - 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            return mat;
        }
    }
}
