using Board;

namespace Chess
{
    class King : Piece
    {
        private ChessGame game;

        public King(GameBoard board, Color color, ChessGame game) : base(board, color)
        {
            this.game = game;
        }

        private bool canMove(Position pos)
        {
            Piece p = gameBoard.piece(pos);
            return p == null || p.color != color;
        }

        private bool testRookForKID(Position pos) // test if the rook is able to make the King's Indian Defense special move
        {
            Piece p = gameBoard.piece(pos);
            return p != null && p is Rook && p.color == color && p.MoveCount == 0;
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

            // #specialmove King's Indian Defense
            if (MoveCount == 0 && !game.Check)
            {
                // Short KID special move
                Position posRook1 = new Position(position.Line, position.Column + 3);

                if (testRookForKID(posRook1))
                {
                    Position p1 = new Position(position.Line, position.Column + 1);
                    Position p2 = new Position(position.Line, position.Column + 2);
                    if (gameBoard.piece(p1) == null && gameBoard.piece(p2) == null)
                    {
                        mat[position.Line, position.Column + 2] = true;
                    }
                }

                // Long KID special move
                Position posRook2 = new Position(position.Line, position.Column - 4);

                if (testRookForKID(posRook2))
                {
                    Position p1 = new Position(position.Line, position.Column - 1);
                    Position p2 = new Position(position.Line, position.Column - 2);
                    Position p3 = new Position(position.Line, position.Column - 3);
                    if (gameBoard.piece(p1) == null && gameBoard.piece(p2) == null && gameBoard.piece(p3) == null)
                    {
                        mat[position.Line, position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
