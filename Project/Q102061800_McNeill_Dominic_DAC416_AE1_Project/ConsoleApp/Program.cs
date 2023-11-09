using System;

using RougeGame;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int vvall;
            //RougeGameUtil.ValidateInput(Console.ReadLine(), out vvall, 1, 5);
            //RougeGameUtil.ConvertIntIntoMoves(vvall);
            RougeGameCore.GameCore();
        }
    }
}