using System;
using Board;
using Chess;

namespace Console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard(8, 8);

            board.placePiece(new Tower(board, Color.Black), new Position(0, 0));
            board.placePiece(new Tower(board, Color.Black), new Position(1, 3));
            board.placePiece(new King(board, Color.Black), new Position(2, 4));

            View.PrintBoard(board);
        }
    }
}
