using System.Collections.Generic;
using Board;

namespace Chess
{
    class ChessGame
    {
        public GameBoard GameBoard { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> gamePieces;
        private HashSet<Piece> captured;

        public ChessGame()
        {
            GameBoard = new GameBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            gamePieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            setGamePieces();
        }

        public void ExecuteMove(Position origin, Position destiny)
        {
            Piece p = GameBoard.removePiece(origin);
            p.incrementMoveCount();
            Piece capturedPiece = GameBoard.removePiece(destiny);
            GameBoard.placePiece(p, destiny);
            if(capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
        }

        public HashSet<Piece> CapturedPiece(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> InGamePiece(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in gamePieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPiece(color));
            return aux;
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

        public void placeNewPiece(char column, int line, Piece piece)
        {
            GameBoard.placePiece(piece, new ChessPosition(column, line).ToPosition());
            gamePieces.Add(piece);
        }

        public void setGamePieces()
        {
            placeNewPiece('d', 8, new Tower(GameBoard, Color.Black));
            placeNewPiece('f', 8, new Tower(GameBoard, Color.Black));
            placeNewPiece('e', 8, new King(GameBoard, Color.Black));
            placeNewPiece('d', 7, new Tower(GameBoard, Color.Black));
            placeNewPiece('e', 7, new Tower(GameBoard, Color.Black));
            placeNewPiece('f', 7, new Tower(GameBoard, Color.Black));

            placeNewPiece('d', 1, new Tower(GameBoard, Color.White));
            placeNewPiece('f', 1, new Tower(GameBoard, Color.White));
            placeNewPiece('e', 1, new King(GameBoard, Color.White));
            placeNewPiece('d', 2, new Tower(GameBoard, Color.White));
            placeNewPiece('e', 2, new Tower(GameBoard, Color.White));
            placeNewPiece('f', 2, new Tower(GameBoard, Color.White));
        }
    }
}
