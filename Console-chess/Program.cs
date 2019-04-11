using System;
using Board;
using Chess;

namespace Console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*                ChessPosition pos = new ChessPosition('c', 8);

                                Console.WriteLine(pos);
                                Console.WriteLine(pos.ToPosition()); */

                ChessGame game = new ChessGame();

                while (!game.Finished)
                {
                    Console.Clear();
                    View.PrintBoard(game.GameBoard);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = View.getChessPosition().ToPosition();

                    bool[,] possibleMove = game.GameBoard.piece(origin).possibleMove();

                    Console.Clear();
                    View.PrintBoard(game.GameBoard, possibleMove);

                    Console.WriteLine();
                    Console.Write("Destiny: ");
                    Position destiny = View.getChessPosition().ToPosition();

                    game.ExecuteMove(origin, destiny);
                }

                View.PrintBoard(game.GameBoard); 
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
