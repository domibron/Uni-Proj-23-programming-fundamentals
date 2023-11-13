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

        public static void DisplayText(string str, ConsoleColor c = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(str);
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

        public static int RandomInt(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        public static Moves ConvertIntIntoMoves(int value)
        {
            //// we get the all the int representations of the Moves values. 0 = Attack.
            //foreach (var move in Enum.GetValues(typeof(Moves)))
            //{
            //    // Check to see if the move is the same as the value.
            //    if ((int)move == value)
            //    {
            //        // returns the enum of the selected value
            //        return (Moves)Enum.Parse(typeof(Moves), Enum.GetName(typeof(Moves), (int)move));
            //    }
            //}

            string? nameOfMove = Enum.GetName(typeof(Moves), (int)value);

            if (nameOfMove != null)
                // returns the enum of the selected value
                return (Moves)Enum.Parse(typeof(Moves), nameOfMove);
            else 
                return Moves.Attack;

            //switch (value)
            //{
            //    case 1:
            //        return Moves.Attack;
            //    case 2:
            //        return Moves.SpecialAttack;
            //    case 3:
            //        return Moves.Recharge;
            //    case 4:
            //        return Moves.Dodge;
            //    case 5:
            //        return Moves.Heal;
            //}

            RougeGameUtil.DisplayText("ERROR IN CONVERSION", ConsoleColor.DarkRed);
            return Moves.Attack;
        }

        public static int StringSize(string text)
        {
            char[] chars = text.ToCharArray();
            return chars.Length + 1;
        }

        public static string PutSpacingInString(string text, int SpaceForText)
        {
            int size = RougeGameUtil.StringSize(text);

            int remaningSpace = SpaceForText - size;

            string spacing = "";

            for (int i = 0; i <= remaningSpace; i++)
            {
                spacing += " ";
            }

            return text + spacing;
        }
    }
}
