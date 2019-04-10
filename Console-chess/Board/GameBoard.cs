namespace Board
{
    class GameBoard
    {
        private Piece[,] _pieces;
        public int Lines { get; set; }
        public int Columns { get; set; }
        
        public GameBoard()
        {
        }

        public GameBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            _pieces = new Piece[lines, columns];
        }

        public Piece piece(int line, int column)
        {
            return _pieces[line, column];
        }

        public Piece piece(Position pos)
        {
            return _pieces[pos.Line, pos.Column];
        }

        public void placePiece(Piece p, Position pos)
        {
            if (testExistPiece(pos))
            {
                throw new BoardException("Can't place there! There is already another piece in that position!");
            }
            _pieces[pos.Line, pos.Column] = p;
            p.position = pos;
        }

        public bool testExistPiece(Position pos)
        {
            testValidPosition(pos);
            return piece(pos) != null;
        }

        public bool testValidPosition(Position pos)
        {
            if(pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }

            return true;
        }

        public void validatePosition(Position pos)
        {
            if (!testValidPosition(pos))
            {
                throw new BoardException("This is not a valid position!");
            }

        }
    }
}
