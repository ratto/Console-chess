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
    }
}
