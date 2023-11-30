using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class RougeGameTest
    {
        public static string[] allInput = { "0", "1", "2", "3", "4", "5", "6", "asd", "", "23.4", "3.7", "-2" };

        public static bool TestAll()
        {
            const int totalTest = 3;
            
            int testCount = 0;

            if (Test_ValidateInput())
            {
                testCount++;
            }

            if (Test_HandlePlayerInput())
            {
                testCount++;
            }

            if (Test_ConvertIntIntoMoves())
            {
                testCount++;
            }

            // checks to see if all tests passed.
            if (testCount == totalTest)
            {
                RougeGameUtil.DisplayText($"All tests passed: {testCount} / {totalTest}", ConsoleColor.DarkGreen);
                return true;
            }
            else
            {
                RougeGameUtil.DisplayText($"one or more tests failed: {testCount} / {totalTest}", ConsoleColor.DarkRed);
                return false;
            }
        }

        public static bool Test_ConvertIntIntoMoves()
        {
            // variables for test cases.
            // Yes, it could be null, but it should not.
            int minValue = (int)Enum.GetValues(typeof(Moves)).GetValue(0);
            int maxValue = (int)Enum.GetValues(typeof(Moves)).GetValue(Enum.GetValues(typeof(Moves)).Length-1);

            if (minValue == null || maxValue == null)
            {
                RougeGameUtil.DisplayText($"CANNOT TEST WITH NULL VALUES, PUT SOME IN HERE!", ConsoleColor.Red);
            }

            // variable used inside testing.
            int testsPassed = 0;
            int intParseValue;

            RougeGameUtil.DisplayText($"Testing convert int into moves", ConsoleColor.Blue);

            foreach (string input in allInput)
            {
                RougeGameUtil.DisplayText($"Test: {input}", ConsoleColor.DarkBlue);

                bool intResult = int.TryParse(input, out intParseValue);

                Moves conformationValue = new();

                Moves result = RougeGameUtil.ConvertIntIntoMoves(intParseValue);

                foreach (int moveInt in Enum.GetValues(typeof(Moves)))
                {
                    if (moveInt == intParseValue)
                    {
                        conformationValue = (Moves)Enum.Parse(typeof(Moves), (string)Enum.GetName(typeof(Moves), (int)moveInt));
                    }
                }



                // Yes, it could be null.
                // Sure, Sure.

                Console.WriteLine(result);

                if (intParseValue <= minValue || intParseValue >= maxValue)
                {
                    RougeGameUtil.DisplayText($"{minValue} {maxValue}");
                }
                else if (result != (Moves)Enum.Parse(typeof(Moves), Enum.GetName(typeof(Moves), Enum.GetValues(typeof(Moves)).GetValue(intParseValue-1))) && (intParseValue < maxValue || intParseValue > minValue))
                {
                    RougeGameUtil.DisplayText($"Test passed: !{result}, !{input}", ConsoleColor.Green);

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    intParseValue = -999;
                }
                // checks if the result is the same as the int parse and the result is within the min and max values.
                else if (result == (Moves)Enum.Parse(typeof(Moves), Enum.GetName(typeof(Moves), Enum.GetValues(typeof(Moves)).GetValue(intParseValue-1))) && (intParseValue < maxValue || intParseValue > minValue))
                {
                    RougeGameUtil.DisplayText($"Test passed: {result} = {input}", ConsoleColor.Green);

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    intParseValue = -999;
                }
                // if both checks fail then the test failed.
                else
                {
                    RougeGameUtil.DisplayText($"Test failed: {result}, {input}", ConsoleColor.Red);

                    intParseValue = -999;
                }
            }


            // conclude test, check all passed.
            int total = allInput.Length;
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

        public static bool Test_HandlePlayerInput()
        {
            // variables for test cases.
            // Yes, it could be null, but it should not.
            int minValue = (int)Enum.GetValues(typeof(Moves)).GetValue(0);
            int maxValue = Enum.GetValues(typeof(Moves)).Length;

            if (minValue == null || maxValue == null)
            {
                RougeGameUtil.DisplayText($"CANNOT TEST WITH NULL VALUES, PUT SOME IN HERE!", ConsoleColor.Red);
            }

            // variable used inside testing.
            int testsPassed = 0;
            int value;
            int intParseValue;

            RougeGameUtil.DisplayText($"Testing handle player input", ConsoleColor.Blue);

            foreach (string input in allInput)
            {
                RougeGameUtil.DisplayText($"Test: {input}", ConsoleColor.DarkBlue);

                int result = RougeGameInputHandling.HandlePlayerInput(input, minValue, maxValue, true, input);

                bool intResult = int.TryParse(input, out intParseValue);

                // Checks if the value is invalidated and outside the min and max values.
                if (result == 0 && (intParseValue < minValue || intParseValue > maxValue))
                {
                    RougeGameUtil.DisplayText($"Test passed: !{result}, !{input}", ConsoleColor.Green);

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // checks if the result is the same as the int parse and the result is within the min and max values.
                else if (result == intParseValue && result <= maxValue && result >= minValue)
                {
                    RougeGameUtil.DisplayText($"Test passed: {result} = {input}", ConsoleColor.Green);

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // if both checks fail then the test failed.
                else
                {
                    RougeGameUtil.DisplayText($"Test failed: {result}, {input}", ConsoleColor.Red);

                    value = -999;
                    intParseValue = -999;
                }
            }


            // conclude test, check all passed.
            int total = allInput.Length;
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

        public static bool Test_ValidateInput()
        {
            // variables for test cases.
            // Yes, it could be null, but it should not.
            int minValue = (int)Enum.GetValues(typeof(Moves)).GetValue(0);
            int maxValue = Enum.GetValues(typeof(Moves)).Length;

            if (minValue == null || maxValue == null)
            {
                RougeGameUtil.DisplayText($"CANNOT TEST WITH NULL VALUES, PUT SOME IN HERE!", ConsoleColor.Red);
            }

            // variable used inside testing.
            int testsPassed = 0;
            int value;
            int intParseValue;


            RougeGameUtil.DisplayText($"Testing validate input", ConsoleColor.Blue);


            foreach (string input in allInput)
            {
                RougeGameUtil.DisplayText($"Test: {input}", ConsoleColor.DarkBlue);
                bool result = RougeGameUtil.ValidateInput(input, out value, minValue, maxValue);

                bool intResult = int.TryParse(input, out intParseValue);

                // this checks if the result is true so it passed validation, int parse marks it as a parse-able value and
                // the value from the validation is within the minValue and maxValue.
                if (result && intResult && value == intParseValue && value <= maxValue && value >= minValue)
                {
                    RougeGameUtil.DisplayText($"Test passed: {value} = {intParseValue}", ConsoleColor.Green);
                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // this checks either both int parse and validate input failed / returned false,
                // or the parsed value failed validation and the validated value is also the same as the in parse value,
                // while the validated value is outside the min and max values.
                else if ((!result && !intResult) || (!result && value == intParseValue && value > maxValue || value < minValue))
                {
                    RougeGameUtil.DisplayText($"Test passed: !{value}, !{intParseValue}", ConsoleColor.Green);
                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // if both cases fail thenm the test failed.
                else
                {
                    RougeGameUtil.DisplayText($"Test failed: {value}, {intParseValue}", ConsoleColor.Red);

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
            }


            // conclude test, check all passed.
            int total = allInput.Length;
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

        // for copy and paste reasons. DO NOT INCLUDE IN BATCH TESTING!
        [Obsolete("Used for programming, not for game runtime.", true)]
        public static bool Test_Base()
        {
            // variables for test cases.
            // Yes, it could be null, but it should not.
            int minValue = (int)Enum.GetValues(typeof(Moves)).GetValue(0);
            int maxValue = Enum.GetValues(typeof(Moves)).Length;

            if (minValue == null || maxValue == null)
            {
                RougeGameUtil.DisplayText($"CANNOT TEST WITH NULL VALUES, PUT SOME IN HERE!", ConsoleColor.Red);
            }

            // variable used inside testing.
            int testsPassed = 0;
            int value;
            int intParseValue;

            RougeGameUtil.DisplayText($"Testing handle player input", ConsoleColor.Blue);

            foreach (string input in allInput)
            {
                RougeGameUtil.DisplayText($"Test: {input}", ConsoleColor.DarkBlue);

                int result = RougeGameInputHandling.HandlePlayerInput(input, minValue, maxValue, true, input);

                bool intResult = int.TryParse(input, out intParseValue);

                // Checks if the value is invalidated and outside the min and max values.
                if (result == 0 && (intParseValue < minValue || intParseValue > maxValue))
                {
                    RougeGameUtil.DisplayText($"Test passed: !{result}, !{input}", ConsoleColor.Green);

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // checks if the result is the same as the int parse and the result is within the min and max values.
                else if (result == intParseValue && result <= maxValue && result >= minValue)
                {
                    RougeGameUtil.DisplayText($"Test passed: {result} = {input}", ConsoleColor.Green);

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // if both checks fail then the test failed.
                else
                {
                    RougeGameUtil.DisplayText($"Test failed: {result}, {input}", ConsoleColor.Red);

                    value = -999;
                    intParseValue = -999;
                }
            }


            // conclude test, check all passed.
            int total = allInput.Length;
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
