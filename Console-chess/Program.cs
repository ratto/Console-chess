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
                ChessGame game = new ChessGame();

                while (!game.Finished)
                {
                    try
                    {
                        Console.Clear();
                        View.PrintBoard(game.GameBoard);

                        Console.WriteLine();
                        Console.WriteLine("Turn: " + game.Turn);
                        Console.WriteLine("Waiting {0} player move...", game.CurrentPlayer);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = View.getChessPosition().ToPosition();
                        game.testValidOrigin(origin);

                        bool[,] possibleMove = game.GameBoard.piece(origin).possibleMove();

                        Console.Clear();
                        View.PrintBoard(game.GameBoard, possibleMove);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = View.getChessPosition().ToPosition();
                        game.testValidDestiny(origin, destiny);

                        game.MakePlay(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}