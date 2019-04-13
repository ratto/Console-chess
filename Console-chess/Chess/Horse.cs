using Board;

namespace Chess
{
    class Horse : Piece
    {
        public Horse(GameBoard board, Color color) : base(board, color)
        {
        }

        private bool canMove(Position pos)
        {
            Piece p = gameBoard.piece(pos);
            return p == null || p.color != color;
        }

        public override string ToString()
        {
            return "H";
        }

        public override bool[,] possibleMove()
        {
            bool[,] mat = new bool[gameBoard.Lines, gameBoard.Columns];

            Position pos = new Position(0, 0);

            //above, right
            pos.definePosition(position.Line - 2, position.Column + 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            //above, left
            pos.definePosition(position.Line - 2, position.Column - 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            //right, above
            pos.definePosition(position.Line - 1, position.Column + 2);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            //right, below
            pos.definePosition(position.Line + 1, position.Column + 2);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            //below, right
            pos.definePosition(position.Line + 2, position.Column + 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            //below, left
            pos.definePosition(position.Line + 2, position.Column - 1);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            //left, above
            pos.definePosition(position.Line - 1, position.Column - 2);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            //left, below
            pos.definePosition(position.Line + 1, position.Column - 2);
            if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

            return mat;
        }
    }
}
