using System;
using Board;

namespace Console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard(8, 8);
            Position p = new Position(3, 4);

            Console.WriteLine("Position: " + p);
        }
    }
}
