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
    }
}
