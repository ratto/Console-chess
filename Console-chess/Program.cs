using System;
using Board;

namespace Console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Position p = new Position(3, 4);

            Console.WriteLine("Position: " + p);
        }
    }
}
