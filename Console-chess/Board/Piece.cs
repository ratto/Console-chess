namespace Board
{
    class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int MoveCount { get; protected set; }
        public GameBoard gameBoard { get; protected set; }

        public Piece()
        {

        }

        public Piece(Position position, Color color, GameBoard gameBoard)
        {
            this.position = position;
            this.color = color;
            this.gameBoard = gameBoard;
            MoveCount = 0;
        }
    }
}
