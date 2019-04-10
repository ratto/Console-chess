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

        public void placePiece(Piece p, Position pos)
        {
            _pieces[pos.Line, pos.Column] = p;
        }
    }
}
