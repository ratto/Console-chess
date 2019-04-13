using Board;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(GameBoard board, Color color) : base(board, color)
        {
        }

        private bool canMove(Position pos)
        {
            Piece p = gameBoard.piece(pos);
            return p == null || p.color != color;
        }

        public override string ToString()
        {
            return "B";
        }

        public override bool[,] possibleMove()
        {
            bool[,] mat = new bool[gameBoard.Lines, gameBoard.Columns];

            Position pos = new Position(0, 0);

            // right + above
            pos.definePosition(position.Line - 1, position.Column + 1);
            while (gameBoard.testValidPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (gameBoard.piece(pos) != null && gameBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.Line--;
                pos.Column++;
            }

            //right + below
            pos.definePosition(position.Line + 1, position.Column + 1);
            while (gameBoard.testValidPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (gameBoard.piece(pos) != null && gameBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.Line++;
                pos.Column++;
            }

            //left + below
            pos.definePosition(position.Line + 1, position.Column - 1);
            while (gameBoard.testValidPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (gameBoard.piece(pos) != null && gameBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.Line++;
                pos.Column--;
            }

            //left + above
            pos.definePosition(position.Line - 1, position.Column - 1);
            while (gameBoard.testValidPosition(pos) && canMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (gameBoard.piece(pos) != null && gameBoard.piece(pos).color != color)
                {
                    break;
                }
                pos.Line--;
                pos.Column--;
            }

            return mat;
        }
    }
}
