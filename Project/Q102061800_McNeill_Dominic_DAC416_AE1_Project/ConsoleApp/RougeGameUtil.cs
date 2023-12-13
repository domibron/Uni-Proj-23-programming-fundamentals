using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.GameMoves;

namespace RougeGame.Util
{
    public class RougeGameUtil
    {
        // displays text normally / without the need of the colour feild.
        public static void DisplayText(string str)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        // displays text with a colour.
        public static void DisplayText(string str, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        // displays text on the same line normally / without the need of a colour feild.
        public static void DisplayTextSameLine(string str)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        // displays text on the same line with a colour.
        public static void DisplayTextSameLine(string str, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        // used to check the input given was valid for the game. (checks if its a whole number in a range of 2 numbers).
        public static bool ValidateInput(string input, out int value, int minValue, int maxValue)
        {
            // used for the out value, it needs to hold a value or it will error.
            value = 0;

            // checks if the string is empty or null.
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // checks if the int parse failed.
            if (!Int32.TryParse(input, out value))
            {
                return false;
            }

            // checks if the value is within the range.
            if (value < minValue || value > maxValue)
            {
                return false;
            }

            // It passed all checks and can return true.
            return true;
        }

        // generates a random int between the set values.
        public static int RandomInt(int min, int max)
        {
            // import random. terrible placement, can move somewhere else. this is inaficiant.
            Random rnd = new Random();
            // add a additional 1 to max value because it only does between min max and not equal to.
            return rnd.Next(min, max+1);
        }

        public static Moves? ConvertIntIntoMoves(int value)
        {
            string? nameOfMove = Enum.GetName(typeof(Moves), (int)value);

            if (nameOfMove != null)
                // returns the enum of the selected value
                return (Moves)Enum.Parse(typeof(Moves), nameOfMove);
            else 
                return null;
        }

        // this returns the sring size of the text.
        public static int StringSize(string text)
        {
            // turns the string into a character arry.
            char[] chars = text.ToCharArray();
            // retrn the length.
            return chars.Length;
        }

        // adds spacing after the text that is a set size. this allows two columns to be alligned properly.
        public static string PutSpacingAfterString(string text, int SpaceForText)
        {
            // gets the size of the string.
            int size = RougeGameUtil.StringSize(text);

            // gaurd clause, if the text is longer then it will overflow.
            if (size >= SpaceForText)
            {
                return text;
            }

            // remove the size of the string from the spacer.
            int remaningSpace = SpaceForText - size;

            // creates a new string for the spacing.
            string spacing = "";

            // repeats for the ammount.
            for (int i = 0; i <= remaningSpace; i++)
            {
                // adds a space to the string.
                spacing += " ";
            }

            // returns the combined string test and spacing.
            return text + spacing;
        }
    }
}
