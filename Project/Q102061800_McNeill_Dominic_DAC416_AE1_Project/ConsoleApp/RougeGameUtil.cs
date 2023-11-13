using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public struct GameAction
    {
        public Moves action;
        public bool heal;

        public GameAction(Moves action = Moves.Attack, bool healed = false)
        {
            this.action = action;
            this.heal = healed;
        }

        public GameAction()
        {
            this.action = Moves.Attack;
            this.heal = false;
        }
    }

    public struct Creature
    {
        public int maxHealth;
        public int health;

        public int maxEnergy;
        public int energy;

        // float to allow percent muliplication 0.5f = 50%
        public float energyRechargeMult;
        //how much to reduce the percent chance to hit. rnd <= 80 + hitChanceAddition.
        public float hitChanceAddition;

        public Creature(int totalHealth, int totalEnergy)
        {
            this.maxHealth = totalHealth;
            this.maxEnergy = totalEnergy;

            this.health = this.maxHealth;
            this.energy = this.maxEnergy;

            // want to add more creatures or have more fights in one game then reset at the start of a new game.
            this.energyRechargeMult = 1;
            this.hitChanceAddition = 0;
        }
        public Creature()
        {
            this.maxHealth = 100;
            this.maxEnergy = 100;

            this.health = this.maxHealth;
            this.energy = this.maxEnergy;

            // want to add more creatures or have more fights in one game then reset at the start of a new game.
            this.energyRechargeMult = 1;
            this.hitChanceAddition = 0;
        }
    }

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

            if (value != null)
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
