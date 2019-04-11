using System;
using Board;
using Chess;

namespace Console_chess
{
    class View
    {
        public static void PrintBoard(GameBoard board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(GameBoard board, bool[,] possibleMove)
        {
            ConsoleColor originalBG = Console.BackgroundColor;
            ConsoleColor changedBG = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMove[i, j])
                    {
                        Console.BackgroundColor = changedBG;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBG;
                    }
                    PrintPiece(board.piece(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = originalBG;
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBG;
        }

        public static ChessPosition getChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + " ");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
