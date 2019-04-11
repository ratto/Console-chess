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

        public override bool[,] possibleMove()
        {
            bool[,] mat = new bool[gameBoard.Lines, gameBoard.Columns];

            Position pos = new Position(0, 0);

            //above
            pos.definePosition(position.Line - 1, position.Column);
            while (gameBoard.testValidPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (gameBoard.piece(pos) != null && gameBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.Line--;
            }

            //below
            pos.definePosition(position.Line + 1, position.Column);
            while (gameBoard.testValidPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (gameBoard.piece(pos) != null && gameBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.Line++;
            }

            //left
            pos.definePosition(position.Line, position.Column - 1);
            while (gameBoard.testValidPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (gameBoard.piece(pos) != null && gameBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.Column--;
            }

            //right
            pos.definePosition(position.Line, position.Column + 1);
            while (gameBoard.testValidPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (gameBoard.piece(pos) != null && gameBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.Column++;
            }

            return mat;
        }
    }
}
