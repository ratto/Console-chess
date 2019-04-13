using Board;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(GameBoard board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool canMove(Position pos)
        {
            return gameBoard.piece(pos) == null;
        }

        private bool canEat(Position pos)
        {
            Piece p = gameBoard.piece(pos);
            return p != null && p.color != color;
        }

        public override bool[,] possibleMove()
        {
            bool[,] mat = new bool[gameBoard.Lines, gameBoard.Columns];

            Position pos = new Position(0, 0);

            if(color == Color.White)
            {
                //above
                pos.definePosition(position.Line - 1, position.Column);
                if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

                // first move
                pos.definePosition(position.Line - 2, position.Column);
                if (gameBoard.testValidPosition(pos) && canMove(pos) && MoveCount == 0) mat[pos.Line, pos.Column] = true;

                // right + above
                pos.definePosition(position.Line - 1, position.Column + 1);
                if (gameBoard.testValidPosition(pos) && canEat(pos)) mat[pos.Line, pos.Column] = true;

                // left + above
                pos.definePosition(position.Line - 1, position.Column - 1);
                if (gameBoard.testValidPosition(pos) && canEat(pos)) mat[pos.Line, pos.Column] = true;
            }
            else
            {
                //below
                pos.definePosition(position.Line + 1, position.Column);
                if (gameBoard.testValidPosition(pos) && canMove(pos)) mat[pos.Line, pos.Column] = true;

                // first move
                pos.definePosition(position.Line + 2, position.Column);
                if (gameBoard.testValidPosition(pos) && canMove(pos) && MoveCount == 0) mat[pos.Line, pos.Column] = true;

                // right + below
                pos.definePosition(position.Line + 1, position.Column + 1);
                if (gameBoard.testValidPosition(pos) && canEat(pos)) mat[pos.Line, pos.Column] = true;

                // left + below
                pos.definePosition(position.Line + 1, position.Column - 1);
                if (gameBoard.testValidPosition(pos) && canEat(pos)) mat[pos.Line, pos.Column] = true;
            }
            

            return mat;
        }
    }
}