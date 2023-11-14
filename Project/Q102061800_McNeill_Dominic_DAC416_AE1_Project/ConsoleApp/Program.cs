using System;

using RougeGame;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RougeGameTest.TestAll();

            int[,] image =
            {
                {0,4,4,4,0,0},
                {4,4,11,11,0,0},
                {4,4,4,4,0,0},
                {0,4,0,4,0,0},
                {0,4,0,4,0,0},
                {0,4,0,4,0,0},
                {0,7,6,7,6,0},
                {0,7,7,8,7,8}
            };

            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), (string)Enum.GetName(typeof(ConsoleColor), image[x, y]));
                    Console.Write("██");

                }
                Console.Write("\n");
            }

            RougeGameCore.GameCore();
        }
    }
}