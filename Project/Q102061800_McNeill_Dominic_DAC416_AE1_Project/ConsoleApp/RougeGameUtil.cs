using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public struct GameAction
    {
        public int intAction;
        public bool heal;

        public GameAction(int action = 0, bool healed = false)
        {
            this.intAction = action;
            this.heal = healed;
        }

        public GameAction()
        {
            this.intAction = 0;
            this.heal = false;
        }
    }

    public struct Creature
    {
        public int maxHealth;
        public int health;

        public int maxEnergy;
        public int energy;

        public float energyRechargeMult;
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
            value = -1;
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
    }
}
