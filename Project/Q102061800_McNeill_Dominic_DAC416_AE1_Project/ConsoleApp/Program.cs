using System;
using RougeGame;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RougeGameTest.TestAll();

            Console.Clear();

            RougeGameUtil.DisplayText("   WELCOME TO\n   ROUGE GAME!");

            int[,] c =
            {
                {5,5,5,5,13,13,13,13},
                {5,5,5,5,13,13,13,13},
                {5,5,5,5,13,13,13,13},
                {5,5,5,5,13,13,13,13},
                {13,13,13,13,5,5,5,5},
                {13,13,13,13,5,5,5,5},
                {13,13,13,13,5,5,5,5},
                {13,13,13,13,5,5,5,5}
            };
            
            RougeGameUI.DrawImageTest(c);

            RougeGameUtil.DisplayText("");
            RougeGameUtil.DisplayText("      MENU");
            RougeGameUtil.DisplayText("");


            RougeGameCore.GameCore();
        }
    }
}