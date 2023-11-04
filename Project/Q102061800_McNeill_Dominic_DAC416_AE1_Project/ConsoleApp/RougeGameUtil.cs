using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class RougeGameUtil
    {
        public static void DisplayText(string str)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayText(string str, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static bool ValidateInput(string input, out int value)
        {
            value = -1;
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (!Int32.TryParse(input, out value))
            {
                return false;
            }

            if (value < 1 || value > 5)
            {
                return false;
            }

            return true;
        }
    }
}
