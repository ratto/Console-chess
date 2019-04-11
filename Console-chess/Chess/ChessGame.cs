using System.Text;
using Board;

namespace Chess
{
    class ChessGame
    {
        public GameBoard GameBoard { get; private set; }
        private int _turn;
        private Color _currentPlayer;
        public bool Finished { get; private set; }

        public ChessGame()
        {
            GameBoard = new GameBoard(8, 8);
            _turn = 1;
            _currentPlayer = Color.White;
            Finished = false;
            setGamePieces();
        }

        public void ExecuteMove(Position origin, Position destiny)
        {
            Piece p = GameBoard.removePiece(origin);
            p.incrementMoveCount();
            Piece capturedPiece = GameBoard.removePiece(destiny);
            GameBoard.placePiece(p, destiny);
        }

        public void setGamePieces()
        {
            GameBoard.placePiece(new Tower(GameBoard, Color.Black), new ChessPosition('a', 8).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.Black), new ChessPosition('h', 8).ToPosition());
            GameBoard.placePiece(new King(GameBoard, Color.Black), new ChessPosition('e', 8).ToPosition());

            GameBoard.placePiece(new Tower(GameBoard, Color.White), new ChessPosition('a', 1).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.White), new ChessPosition('h', 1).ToPosition());
            GameBoard.placePiece(new King(GameBoard, Color.White), new ChessPosition('e', 1).ToPosition());
        }
    }
}
