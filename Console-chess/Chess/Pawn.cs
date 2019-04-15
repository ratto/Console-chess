using Board;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessGame game;

        public Pawn(GameBoard board, Color color, ChessGame game) : base(board, color)
        {
            this.game = game;
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

            if (color == Color.White) // Whites
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

                // #specialmove En Passant
                if (position.Line == 3)
                {
                    Position left = new Position(position.Line, position.Column - 1);
                    Position right = new Position(position.Line, position.Column + 1);
                    if (gameBoard.testValidPosition(left) && canEat(left) && gameBoard.piece(left) == game.enPassantDanger)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    if (gameBoard.testValidPosition(right) && canEat(right) && gameBoard.piece(right) == game.enPassantDanger)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else // Blacks
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

                // #specialmove En Passant
                if (position.Line == 4)
                {
                    Position left = new Position(position.Line, position.Column - 1);
                    Position right = new Position(position.Line, position.Column + 1);
                    if (gameBoard.testValidPosition(left) && canEat(left) && gameBoard.piece(left) == game.enPassantDanger)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    if (gameBoard.testValidPosition(right) && canEat(right) && gameBoard.piece(right) == game.enPassantDanger)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }


            return mat;
        }
    }
}