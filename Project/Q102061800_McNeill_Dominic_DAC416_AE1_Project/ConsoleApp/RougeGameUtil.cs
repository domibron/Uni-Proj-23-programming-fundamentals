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
        public static void DisplayText(string str)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayText(string str, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayTextSameLine(string str)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayTextSameLine(string str, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static bool ValidateInput(string input, out int value, int minValue, int maxValue)
        {
            value = 0;
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (!Int32.TryParse(input, out value))
            {
                return false;
            }

            if (value < minValue || value > maxValue)
            {
                return false;
            }

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
            return chars.Length + 1;
        }

        // adds spacing after the text that is a set size. this allows two columns to be alligned properly.
        public static string PutSpacingInString(string text, int SpaceForText)
        {
            // gets the size of the string.
            int size = RougeGameUtil.StringSize(text);

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
