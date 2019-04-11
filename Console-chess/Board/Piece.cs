namespace Board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int MoveCount { get; protected set; }
        public GameBoard gameBoard { get; protected set; }

        public Piece()
        {

        }

        public Piece(GameBoard gameBoard, Color color)
        {
            this.position = null;
            this.color = color;
            this.gameBoard = gameBoard;
            MoveCount = 0;
        }

        public void incrementMoveCount()
        {
            MoveCount++;
        }

        protected bool canMove(Position pos)
        {
            Piece p = gameBoard.piece(pos);
            return p == null || p.color != color;
        }

        public bool testPossibleMoves()
        {
            bool[,] mat = possibleMove();
            for (int i = 0; i < gameBoard.Lines; i++)
            {
                for (int j=0; j< gameBoard.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos)
        {
            return possibleMove()[pos.Line, pos.Column];
        }

        public abstract bool[,] possibleMove();
    }
}
