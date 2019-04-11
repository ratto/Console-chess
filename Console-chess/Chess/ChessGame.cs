using System.Text;
using Board;

namespace Chess
{
    class ChessGame
    {
        public GameBoard GameBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessGame()
        {
            GameBoard = new GameBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
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

        public void MakePlay(Position origin, Position destiny)
        {
            ExecuteMove(origin, destiny);
            Turn++;
            changePlayer();
        }

        public void testValidOrigin(Position pos)
        {
            if(GameBoard.piece(pos) == null)
            {
                throw new BoardException("There are no piece in that position.");
            }
            if (CurrentPlayer != GameBoard.piece(pos).color)
            {
                throw new BoardException("That piece is not yours.");
            }
            if (!GameBoard.piece(pos).testPossibleMoves())
            {
                throw new BoardException("There are no possible moves for the chosen piece.");
            }
        }

        public void testValidDestiny(Position origin, Position destiny)
        {
            if (!GameBoard.piece(origin).canMoveTo(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }

        private void changePlayer()
        {
            if (CurrentPlayer == Color.White) CurrentPlayer = Color.Black;
            else CurrentPlayer = Color.White;
        }

        public void setGamePieces()
        {
            GameBoard.placePiece(new Tower(GameBoard, Color.Black), new ChessPosition('d', 8).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.Black), new ChessPosition('f', 8).ToPosition());
            GameBoard.placePiece(new King(GameBoard, Color.Black), new ChessPosition('e', 8).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.Black), new ChessPosition('d', 7).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.Black), new ChessPosition('f', 7).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.Black), new ChessPosition('e', 7).ToPosition());

            GameBoard.placePiece(new Tower(GameBoard, Color.White), new ChessPosition('d', 1).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.White), new ChessPosition('f', 1).ToPosition());
            GameBoard.placePiece(new King(GameBoard, Color.White), new ChessPosition('e', 1).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.White), new ChessPosition('d', 2).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.White), new ChessPosition('f', 2).ToPosition());
            GameBoard.placePiece(new Tower(GameBoard, Color.White), new ChessPosition('e', 2).ToPosition());
        }
    }
}
