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
        public bool Check { get; private set; }
        public Piece enPassantDanger { get; private set; }

        public ChessGame()
        {
            GameBoard = new GameBoard(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Check = false;
            Finished = false;
            gamePieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            enPassantDanger = null;
            setGamePieces();
        }

        public HashSet<Piece> CapturedPiece(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        private Color rival(Color color)
        {
            if (color == Color.Black) return Color.White;
            else return Color.Black;
        }

        private Piece king(Color color)
        {
            foreach (Piece x in InGamePiece(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool KingInCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
            {
                throw new BoardException("There is no " + color + " king on the game board!");
            }

            foreach (Piece x in InGamePiece(rival(color)))
            {
                bool[,] mat = x.possibleMove();
                if (mat[K.position.Line, K.position.Column]) return true;
            }
            return false;
        }

        public bool testCheckMate(Color color)
        {
            if (!KingInCheck(color))
            {
                return false;
            }
            foreach (Piece x in InGamePiece(color))
            {
                bool[,] mat = x.possibleMove();
                for (int i = 0; i < GameBoard.Lines; i++)
                {
                    for (int j = 0; j < GameBoard.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ExecuteMove(origin, destiny);
                            bool testCheck = KingInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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

        public Piece ExecuteMove(Position origin, Position destiny)
        {
            Piece p = GameBoard.removePiece(origin);
            p.incrementMoveCount();
            Piece capturedPiece = GameBoard.removePiece(destiny);
            GameBoard.placePiece(p, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // #specialmove short King's Indian Defense
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Line, origin.Column + 3);
                Position destinyRook = new Position(origin.Line, origin.Column + 1);
                Piece rook = GameBoard.removePiece(originRook);
                rook.incrementMoveCount();
                GameBoard.placePiece(rook, destinyRook);
            }

            // #specialmove long King's Indian Defense
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Line, origin.Column - 4);
                Position destinyRook = new Position(origin.Line, origin.Column - 1);
                Piece rook = GameBoard.removePiece(originRook);
                rook.incrementMoveCount();
                GameBoard.placePiece(rook, destinyRook);
            }

            // #specialmove En Passant
            if(p is Pawn)
            {
                if(origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position posPawn;
                    if(p.color == Color.White)
                    {
                        posPawn = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else
                    {
                        posPawn = new Position(destiny.Line - 1, destiny.Column);
                    }

                    capturedPiece = GameBoard.removePiece(posPawn);
                    captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = GameBoard.removePiece(destiny);
            p.decrementMoveCount();
            if (capturedPiece != null)
            {
                GameBoard.placePiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            GameBoard.placePiece(p, origin);

            // #specialmove short King's Indian Defense
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originRook = new Position(origin.Line, origin.Column + 3);
                Position destinyRook = new Position(origin.Line, origin.Column + 1);
                Piece rook = GameBoard.removePiece(destinyRook);
                rook.decrementMoveCount();
                GameBoard.placePiece(rook, originRook);
            }

            // #specialmove long King's Indian Defense
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originRook = new Position(origin.Line, origin.Column - 4);
                Position destinyRook = new Position(origin.Line, origin.Column - 1);
                Piece rook = GameBoard.removePiece(destinyRook);
                rook.decrementMoveCount();
                GameBoard.placePiece(rook, originRook);
            }

            // #specialmove En Passant
            if(p is Pawn)
            {
                if(origin.Column != destiny.Column && capturedPiece == enPassantDanger)
                {
                    Piece pawn = GameBoard.removePiece(destiny);
                    Position posPawn;
                    if(p.color == Color.White)
                    {
                        posPawn = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posPawn = new Position(4, destiny.Column);
                    }
                    GameBoard.placePiece(pawn, posPawn);
                }
            }
        }

        public void MakePlay(Position origin, Position destiny)
        {
            Piece piece = ExecuteMove(origin, destiny);

            if (KingInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, piece);
                throw new BoardException("You can't put yourself in check!");
            }

            Piece p = GameBoard.piece(destiny);

            // #specialmove Promote pawn
            if(p is Pawn)
            {
                if((p.color == Color.White && destiny.Line == 0) || (p.color == Color.Black && destiny.Line == 7))
                {
                    p = GameBoard.removePiece(destiny);
                    gamePieces.Remove(p);
                    Piece queen = new Queen(GameBoard, p.color);
                    GameBoard.placePiece(queen, destiny);
                    gamePieces.Add(queen);
                }
            }

            if (KingInCheck(rival(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (testCheckMate(rival(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                changePlayer();
            }

            // #specialmove En Passant
            if(p is Pawn && (destiny.Line == origin.Line + 2 || destiny.Line == origin.Line - 2))
            {
                enPassantDanger = p;
            }
            else
            {
                enPassantDanger = null;
            }
        }

        public void testValidOrigin(Position pos)
        {
            if (GameBoard.piece(pos) == null)
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
            // blacks
            placeNewPiece('a', 8, new Rook(GameBoard, Color.Black));
            placeNewPiece('b', 8, new Knight(GameBoard, Color.Black));
            placeNewPiece('c', 8, new Bishop(GameBoard, Color.Black));
            placeNewPiece('d', 8, new Queen(GameBoard, Color.Black));
            placeNewPiece('e', 8, new King(GameBoard, Color.Black, this));
            placeNewPiece('f', 8, new Bishop(GameBoard, Color.Black));
            placeNewPiece('g', 8, new Knight(GameBoard, Color.Black));
            placeNewPiece('h', 8, new Rook(GameBoard, Color.Black));

            placeNewPiece('a', 7, new Pawn(GameBoard, Color.Black, this));
            placeNewPiece('b', 7, new Pawn(GameBoard, Color.Black, this));
            placeNewPiece('c', 7, new Pawn(GameBoard, Color.Black, this));
            placeNewPiece('d', 7, new Pawn(GameBoard, Color.Black, this));
            placeNewPiece('e', 7, new Pawn(GameBoard, Color.Black, this));
            placeNewPiece('f', 7, new Pawn(GameBoard, Color.Black, this));
            placeNewPiece('g', 7, new Pawn(GameBoard, Color.Black, this));
            placeNewPiece('h', 7, new Pawn(GameBoard, Color.Black, this));

            // whites
            placeNewPiece('a', 1, new Rook(GameBoard, Color.White));
            placeNewPiece('b', 1, new Knight(GameBoard, Color.White));
            placeNewPiece('c', 1, new Bishop(GameBoard, Color.White));
            placeNewPiece('d', 1, new Queen(GameBoard, Color.White));
            placeNewPiece('e', 1, new King(GameBoard, Color.White, this));
            placeNewPiece('f', 1, new Bishop(GameBoard, Color.White));
            placeNewPiece('g', 1, new Knight(GameBoard, Color.White));
            placeNewPiece('h', 1, new Rook(GameBoard, Color.White));

            placeNewPiece('a', 2, new Pawn(GameBoard, Color.White, this));
            placeNewPiece('b', 2, new Pawn(GameBoard, Color.White, this));
            placeNewPiece('c', 2, new Pawn(GameBoard, Color.White, this));
            placeNewPiece('d', 2, new Pawn(GameBoard, Color.White, this));
            placeNewPiece('e', 2, new Pawn(GameBoard, Color.White, this));
            placeNewPiece('f', 2, new Pawn(GameBoard, Color.White, this));
            placeNewPiece('g', 2, new Pawn(GameBoard, Color.White, this));
            placeNewPiece('h', 2, new Pawn(GameBoard, Color.White, this));
        }
    }
}
