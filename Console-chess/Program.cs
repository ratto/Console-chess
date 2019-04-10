using System;
using Board;

namespace Console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard(8, 8);

            View.PrintBoard(board);
        }
    }
}
