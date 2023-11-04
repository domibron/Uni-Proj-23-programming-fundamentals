using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class RougeGameTest
    {
        public static bool Text_ValidateInput()
        {
            int testsPassed = 0;
            int value;

            RougeGameUtil.DisplayText("Testing validate input", ConsoleColor.Blue);


            RougeGameUtil.DisplayText("test: 0", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("0", out value))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }


            RougeGameUtil.DisplayText("test: 1", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("1", out value))
            {
                if (value == 1)
                {
                    testsPassed++;
                    RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
                }
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }


            RougeGameUtil.DisplayText("test: 2", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("2", out value) && value == 2)
            {
                if (value == 2)
                {
                    testsPassed++;
                    RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
                }
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }


            RougeGameUtil.DisplayText("test: 3", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("3", out value) && value == 3)
            {
                if (value == 3)
                {
                    testsPassed++;
                    RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
                }
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }


            RougeGameUtil.DisplayText("test: 4", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("4", out value) && value == 4)
            {
                if (value == 4)
                {
                    testsPassed++;
                    RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
                }
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }


            RougeGameUtil.DisplayText("test: 5", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("5", out value) && value == 5)
            {
                if (value == 5)
                {
                    testsPassed++;
                    RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
                }
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }


            RougeGameUtil.DisplayText("test: 6", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("6", out value))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }


            RougeGameUtil.DisplayText("test: asdfg", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("asdfg", out value))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }


            RougeGameUtil.DisplayText("test: ", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("", out value))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("test: failed", ConsoleColor.Red);
            }



            if (testsPassed == 9)
            {
                RougeGameUtil.DisplayText("all tests passed", ConsoleColor.DarkGreen);
                return true;
            }
            else
            {
                RougeGameUtil.DisplayText($"faliure of test. tests passed: {testsPassed}", ConsoleColor.DarkRed);
                return false;
            }

        }
    }
}
