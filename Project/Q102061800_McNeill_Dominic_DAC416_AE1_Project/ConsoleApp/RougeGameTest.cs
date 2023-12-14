using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.Util;
using RougeGame.LogSystem;
using RougeGame.GameMoves;

namespace RougeGame.Test
{
    public class RougeGameTest
    {
        // what we are using to test.
        public static string[] allInput = { "0", "1", "2", "3", "4", "5", "6", "asd", "", "23.4", "3.7", "-2" };

        // used to run all tests.
        public static bool TestAll()
        {
            // log the start.
            RougeGameLogSystem.Instance.WriteLine("START OF TESTING");

            // how many tests are there.
            const int totalTest = 3;
            
            int testPassedCount = 0;

            // if validate input passed.
            if (Test_ValidateInput())
            {
                testPassedCount++;
            }

            // if handle player input passed.
            if (Test_HandlePlayerInput())
            {
                testPassedCount++;
            }

            // if vconvert int into moves passed.
            if (Test_ConvertIntIntoMoves())
            {
                testPassedCount++;
            }

            // checks to see if all tests passed.
            if (testPassedCount == totalTest)
            {
                RougeGameLogSystem.Instance.WriteLine($"All tests passed: {testPassedCount} / {totalTest}");
                 RougeGameLogSystem.Instance.WriteLine("END OF TESTING");
                return true;
            }
            else
            {
                RougeGameLogSystem.Instance.WriteLine($"one or more tests failed: {testPassedCount} / {totalTest}");
                 RougeGameLogSystem.Instance.WriteLine("END OF TESTING");
                return false;
            }
        }

        public static bool Test_ConvertIntIntoMoves()
        {
            // variables for test cases.
            // Yes, it could be null, but it should not.
            int minValue = (int)Enum.GetValues(typeof(Moves)).GetValue(0);
            int maxValue = (int)Enum.GetValues(typeof(Moves)).GetValue(Enum.GetValues(typeof(Moves)).Length - 1);

            if (minValue == null || maxValue == null)
            {
                RougeGameLogSystem.Instance.WriteLine($"CANNOT TEST WITH NULL VALUES, PUT SOME IN HERE!");
            }

            // variable used inside testing.
            int testsPassed = 0;
            int intParseValue;

            RougeGameLogSystem.Instance.WriteLine($"Testing convert int into moves");

            foreach (string input in allInput)
            {
                RougeGameLogSystem.Instance.WriteLine($"Test: {input}");

                bool intResult = int.TryParse(input, out intParseValue);

                Moves? result = RougeGameUtil.ConvertIntIntoMoves(intParseValue);
                
                Moves? moveConvert;

                try 
                { 
                    moveConvert = (Moves)Enum.Parse(typeof(Moves), Enum.GetName(typeof(Moves), int.Parse(input)));
                }
                catch
                {
                    moveConvert = null;
                }
                

                if (moveConvert == null && result == null)
                {
                    RougeGameLogSystem.Instance.WriteLine($"null = null");
                    testsPassed++;
                }
                else if (moveConvert == result)
                {
                    RougeGameLogSystem.Instance.WriteLine($"{moveConvert} = {result}");
                    testsPassed++;
                }
                else
                {
                    RougeGameLogSystem.Instance.WriteLine($"{moveConvert} = {result}");
                }


                // reset values
                moveConvert = null;
                intParseValue = -999;
            }


            // conclude test, check all passed.
            int total = allInput.Length;
            if (testsPassed == total)
            {
                RougeGameLogSystem.Instance.WriteLine($"All tests passed. tests passed: {testsPassed} / {total}");
                return true;
            }
            else
            {
                RougeGameLogSystem.Instance.WriteLine($"Failure of test. tests passed: {testsPassed} / {total}");
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
                RougeGameLogSystem.Instance.WriteLine($"CANNOT TEST WITH NULL VALUES, PUT SOME IN HERE!");
            }

            // variable used inside testing.
            int testsPassed = 0;
            int value;
            int intParseValue;

            RougeGameLogSystem.Instance.WriteLine($"Testing handle player input");

            foreach (string input in allInput)
            {
                RougeGameLogSystem.Instance.WriteLine($"Test: {input}");

                int result = RougeGameInputHandling.HandlePlayerInput(input, minValue, maxValue, input);

                bool intResult = int.TryParse(input, out intParseValue);

                // Checks if the value is invalidated and outside the min and max values.
                if (result == 0 && (intParseValue < minValue || intParseValue > maxValue))
                {
                    RougeGameLogSystem.Instance.WriteLine($"Test passed: !{result}, !{input}");

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // checks if the result is the same as the int parse and the result is within the min and max values.
                else if (result == intParseValue && result <= maxValue && result >= minValue)
                {
                    RougeGameLogSystem.Instance.WriteLine($"Test passed: {result} = {input}");

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // if both checks fail then the test failed.
                else
                {
                    RougeGameLogSystem.Instance.WriteLine($"Test failed: {result}, {input}");

                    value = -999;
                    intParseValue = -999;
                }
            }


            // conclude test, check all passed.
            int total = allInput.Length;
            if (testsPassed == total)
            {
                RougeGameLogSystem.Instance.WriteLine($"All tests passed. tests passed: {testsPassed} / {total}");
                return true;
            }
            else
            {
                RougeGameLogSystem.Instance.WriteLine($"Failure of test. tests passed: {testsPassed} / {total}");
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
                RougeGameLogSystem.Instance.WriteLine($"CANNOT TEST WITH NULL VALUES, PUT SOME IN HERE!");
            }

            // variable used inside testing.
            int testsPassed = 0;
            int value;
            int intParseValue;


            RougeGameLogSystem.Instance.WriteLine($"Testing validate input");


            foreach (string input in allInput)
            {
                RougeGameLogSystem.Instance.WriteLine($"Test: {input}");
                bool result = RougeGameUtil.ValidateInput(input, out value, minValue, maxValue);

                bool intResult = int.TryParse(input, out intParseValue);

                // this checks if the result is true so it passed validation, int parse marks it as a parse-able value and
                // the value from the validation is within the minValue and maxValue.
                if (result && intResult && value == intParseValue && value <= maxValue && value >= minValue)
                {
                    RougeGameLogSystem.Instance.WriteLine($"Test passed: {value} = {intParseValue}");
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
                    RougeGameLogSystem.Instance.WriteLine($"Test passed: !{value}, !{intParseValue}");
                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // if both cases fail thenm the test failed.
                else
                {
                    RougeGameLogSystem.Instance.WriteLine($"Test failed: {value}, {intParseValue}");

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
            }


            // conclude test, check all passed.
            int total = allInput.Length;
            if (testsPassed == total)
            {
                RougeGameLogSystem.Instance.WriteLine($"All tests passed. tests passed: {testsPassed} / {total}");
                return true;
            }
            else
            {
                RougeGameLogSystem.Instance.WriteLine($"Failure of test. tests passed: {testsPassed} / {total}");
                return false;
            }

        }

        // for copy and paste reasons. DO NOT INCLUDE IN BATCH TESTING!
        // this marks it to not be used, the true will make the code using this throw and exeption.
        [Obsolete("Used for programming, not for game runtime.", true)]
        public static bool Test_Base()
        {
            // variables for test cases.
            // Yes, it could be null, but it should not.
            int minValue = (int)Enum.GetValues(typeof(Moves)).GetValue(0);
            int maxValue = Enum.GetValues(typeof(Moves)).Length;

            if (minValue == null || maxValue == null)
            {
                RougeGameLogSystem.Instance.WriteLine($"CANNOT TEST WITH NULL VALUES, PUT SOME IN HERE!");
            }

            // variable used inside testing.
            int testsPassed = 0;
            int value;
            int intParseValue;

            RougeGameLogSystem.Instance.WriteLine($"Testing handle player input");

            foreach (string input in allInput)
            {
                RougeGameLogSystem.Instance.WriteLine($"Test: {input}");

                int result = RougeGameInputHandling.HandlePlayerInput(input, minValue, maxValue, input);

                bool intResult = int.TryParse(input, out intParseValue);

                // Checks if the value is invalidated and outside the min and max values.
                if (result == 0 && (intParseValue < minValue || intParseValue > maxValue))
                {
                    RougeGameLogSystem.Instance.WriteLine($"Test passed: !{result}, !{input}");

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // checks if the result is the same as the int parse and the result is within the min and max values.
                else if (result == intParseValue && result <= maxValue && result >= minValue)
                {
                    RougeGameLogSystem.Instance.WriteLine($"Test passed: {result} = {input}");

                    testsPassed++;

                    // reset values incase the stored value is also a pass case for the next test.
                    value = -999;
                    intParseValue = -999;
                }
                // if both checks fail then the test failed.
                else
                {
                    RougeGameLogSystem.Instance.WriteLine($"Test failed: {result}, {input}");

                    value = -999;
                    intParseValue = -999;
                }
            }


            // conclude test, check all passed.
            int total = allInput.Length;
            if (testsPassed == total)
            {
                RougeGameLogSystem.Instance.WriteLine($"All tests passed. tests passed: {testsPassed} / {total}");
                return true;
            }
            else
            {
                RougeGameLogSystem.Instance.WriteLine($"Failure of test. tests passed: {testsPassed} / {total}");
                return false;
            }
        }
    }
}
