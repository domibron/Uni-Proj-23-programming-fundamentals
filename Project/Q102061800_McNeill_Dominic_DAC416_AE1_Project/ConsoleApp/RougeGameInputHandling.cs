using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class InputHandling
    {
        // could redo to be more flexible.
        public static int HandlePlayerInput(string msg, int minValue, int maxValue)
        {
            RougeGameUtil.DisplayText(msg);

            string? input = Console.ReadLine();

            int value;

            if (RougeGameUtil.ValidateInput(input, out value, minValue, maxValue))
            {
                return value;
            }

            return 0;
        }

        public static GameAction PlayerInput(Creature playerCreature, string displayInfo = "")
        {
            GameAction returnValue = new GameAction();

            bool waitingForValidInput = true;
            int energyCalc = playerCreature.energy;

            const int minChoice = 1;
            const int maxChoice = 5;

            const int attackCost = 5;
            const int specialAttackCost = 20;
            const int healCost = 10;

            while (waitingForValidInput)
            {

                if (energyCalc >= attackCost)
                {
                    RougeGameUtil.DisplayText("[1] Attack");
                }
                else
                {
                    RougeGameUtil.DisplayText("[1] Attack", ConsoleColor.DarkRed);
                }

                if (energyCalc >= specialAttackCost)
                {
                    RougeGameUtil.DisplayText("[2] Special Attack");
                }
                else
                {
                    RougeGameUtil.DisplayText("[2] Special Attack", ConsoleColor.DarkRed);
                }

                RougeGameUtil.DisplayText("[3] Recharge");
                RougeGameUtil.DisplayText("[4] Dodge");

                if (!returnValue.heal && energyCalc >= healCost)
                {
                    RougeGameUtil.DisplayText("[5] Heal");
                }
                else if (returnValue.heal)
                {
                    RougeGameUtil.DisplayText("[5] Heal", ConsoleColor.Blue);
                }
                else
                {
                    RougeGameUtil.DisplayText("[5] Heal", ConsoleColor.DarkRed);
                }
                

                int value = InputHandling.HandlePlayerInput("\nAction:", minChoice, maxChoice);

                if (value == 0) // I dont want to automattically pick attack. I want the player to pick.
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText($"\nPICK A VALID OPTION!\n", ConsoleColor.Red);
                    continue;
                }

                Moves move = RougeGameUtil.ConvertIntIntoMoves(value);


                // this hurts my eyes! AH!

                /* I think maybe redo / make it more readable
                 * Can any of this go into functions.
                 * also relook at the flow chart
                 */
                
                if (move == Moves.Heal && !returnValue.heal && energyCalc >= healCost)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    returnValue.heal = true;
                    energyCalc /= 2;
                }
                else if (move == Moves.Heal && returnValue.heal)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText("\nYOU ALREADY CHOSEN HEAL, BUT YOU CAN SELECT ANOTHER MOVE\n", ConsoleColor.Red);
                }
                else if (move == Moves.Attack && energyCalc >= attackCost)
                {
                    returnValue.action = move;
                    waitingForValidInput = false;
                }
                else if (move == Moves.SpecialAttack && energyCalc >= specialAttackCost)
                {
                    returnValue.action = move;
                    waitingForValidInput = false;
                }
                else if (move == Moves.Recharge || move == Moves.Dodge)
                {
                    returnValue.action = move;
                    waitingForValidInput = false;
                }
                else
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText($"\nPICK A VALID OPTION!\n", ConsoleColor.Red);
                }

            }

            return returnValue;
        }

        public static GameAction ComputerInput(Creature computerCreature)
        {
            GameAction action = new GameAction();

            int errorCont = 0;

            bool inLoop = true;

            const int minChoice = 1;
            const int maxChoice = 5;

            const int attackCost = 5;
            const int specialAttackCost = 20;
            const int healCost = 10;

            while (inLoop)
            {
                if (errorCont == 5)
                {
                    throw new Exception($"Computer failed to pick action, Iteration {errorCont}");
                }

                int computerChoice = RougeGameUtil.RandomInt(minChoice, maxChoice);

                Moves move = RougeGameUtil.ConvertIntIntoMoves(computerChoice);

                if (move == Moves.Heal && !action.heal && computerCreature.health <= 80 && computerCreature.energy >= healCost)
                {
                    action.heal = true;
                }
                //else if (computerChoice == 5 && action.heal)
                //{
                //    // pick again
                //}
                else if (move == Moves.Attack && computerCreature.energy >= attackCost)
                {
                    action.action = move;
                    inLoop = false;
                }
                else if (move == Moves.SpecialAttack && computerCreature.energy >= specialAttackCost)
                {
                    action.action = move;
                    inLoop = false;
                }
                else if (move == Moves.Recharge || move == Moves.Dodge)
                {
                    action.action = move;
                    inLoop = false;
                }
                else
                {
                    // pick again
                    errorCont++;
                    RougeGameUtil.DisplayText("Computer failed", ConsoleColor.Yellow);
                }
            }

            return action;
        }
    }
}
