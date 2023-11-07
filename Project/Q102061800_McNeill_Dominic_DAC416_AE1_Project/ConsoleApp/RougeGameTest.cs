using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class RougeGameTest
    {
        public static bool TestAll()
        {
            return false;
        }

        public static bool Text_ValidateInput()
        {
            int testsPassed = 0;
            int value;

            const int minValue = 1;
            const int maxValue = 5;

            RougeGameUtil.DisplayText("Testing validate input", ConsoleColor.Blue);


            // Test for input of 0, should result in false
            RougeGameUtil.DisplayText("Test: 0", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("0", out value, minValue, maxValue))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of 1, should result in true
            RougeGameUtil.DisplayText("Test: 1", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("1", out value, minValue, maxValue) && value == 1)
            {
                    testsPassed++;
                    RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);

            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of 2, should result in true
            RougeGameUtil.DisplayText("Test: 2", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("2", out value, minValue, maxValue) && value == 2)
            {
                    testsPassed++;
                    RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of 3, should result in true
            RougeGameUtil.DisplayText("Test: 3", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("3", out value, minValue, maxValue) && value == 3)
            {
                    testsPassed++;
                    RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of 4, should result in true
            RougeGameUtil.DisplayText("Test: 4", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("4", out value, minValue, maxValue) && value == 4)
            {
                    testsPassed++;
                    RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of 5, should result in true
            RougeGameUtil.DisplayText("Test: 5", ConsoleColor.DarkBlue);
            if (RougeGameUtil.ValidateInput("5", out value, minValue, maxValue) && value == 5)
            {
                    testsPassed++;
                    RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of 6, should result in false
            RougeGameUtil.DisplayText("Test: 6", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("6", out value, minValue, maxValue))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of asdfg, should result in false
            RougeGameUtil.DisplayText("Test: asdfg", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("asdfg", out value, minValue, maxValue))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of "", should result in false
            RougeGameUtil.DisplayText("Test: \"\"", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("", out value, minValue, maxValue))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of 5.5, should result in false
            RougeGameUtil.DisplayText("Test: 5.5", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("5.5", out value, minValue, maxValue))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // Test for input of 4.5, should result in false
            RougeGameUtil.DisplayText("Test: 4.5", ConsoleColor.DarkBlue);
            if (!RougeGameUtil.ValidateInput("4.5", out value, minValue, maxValue))
            {
                testsPassed++;
                RougeGameUtil.DisplayText("Test: passed", ConsoleColor.Green);
            }
            else
            {
                RougeGameUtil.DisplayText("Test: failed", ConsoleColor.Red);
            }


            // conclude test, check all passed.
            int total = 11;
            if (testsPassed == total)
            {
                RougeGameUtil.DisplayText($"All tests passed. tests passed: {testsPassed} / {total}", ConsoleColor.DarkGreen);
                return true;
            }
            else
            {
                RougeGameUtil.DisplayText($"Faliure of test. tests passed: {testsPassed} / {total}", ConsoleColor.DarkRed);
                return false;
            }

        }
    }
}
