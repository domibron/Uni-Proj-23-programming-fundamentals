using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeGame
{
    public class RougeGameInputHandling
    {
       // doing // could redo to be more flexible.
        public static int HandlePlayerInput(string msg, int minValue, int maxValue, bool debug = false, string forcedInput = "")
        {
            RougeGameUtil.DisplayText(msg);

            string? input = "";

            if (!debug)
            {
                input = Console.ReadLine();
            }
            else
            {
                input = forcedInput;
            }

            int value;

            // no, the input should have a value.
            if (RougeGameUtil.ValidateInput(input, out value, minValue, maxValue))
            {
                return value;
            }

            return 0;
        }

        public static GameAction PlayerInput(int playerCreatureEnergy, string displayInfo = "")
        {
            GameAction returnValue = new GameAction();

            bool waitingForValidInput = true;

            const int minChoice = 1;
            const int maxChoice = 5;

            const int attackCost = 5;
            const int specialAttackCost = 20;
            const int healCost = 10;

            while (waitingForValidInput)
            {

                if (playerCreatureEnergy >= attackCost)
                {
                    RougeGameUtil.DisplayText("[1] Attack");
                }
                else
                {
                    RougeGameUtil.DisplayText("[1] Attack", ConsoleColor.DarkRed);
                }

                if (playerCreatureEnergy >= specialAttackCost)
                {
                    RougeGameUtil.DisplayText("[2] Special Attack");
                }
                else
                {
                    RougeGameUtil.DisplayText("[2] Special Attack", ConsoleColor.DarkRed);
                }

                RougeGameUtil.DisplayText("[3] Recharge");
                RougeGameUtil.DisplayText("[4] Dodge");

                if (!returnValue.heal && playerCreatureEnergy >= healCost)
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


                int value = RougeGameInputHandling.HandlePlayerInput("\nAction:", minChoice, maxChoice);
                

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
                
                if (move == Moves.Heal && !returnValue.heal && playerCreatureEnergy >= healCost)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    returnValue.heal = true;
                    playerCreatureEnergy -= healCost;
                    playerCreatureEnergy /= 2;
                }
                else if (move == Moves.Heal && returnValue.heal)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText("\nYOU ALREADY CHOSEN HEAL, BUT YOU CAN SELECT ANOTHER MOVE\n", ConsoleColor.Red);
                }
                else if (move == Moves.Attack && playerCreatureEnergy >= attackCost)
                {
                    returnValue.action = move;
                    waitingForValidInput = false;
                }
                else if (move == Moves.SpecialAttack && playerCreatureEnergy >= specialAttackCost)
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

        public static GameAction ComputerInput(CreatureBase computerCreature)
        {
            GameAction action = new GameAction();

            int errorCont = 0;

            bool inLoop = true;

            //const int minChoice = 1;
            //const int maxChoice = 5;

            //const int attackCost = 5;
            //const int specialAttackCost = 20;
            //const int healCost = 10;

            while (inLoop)
            {
                //if (errorCont == 5)
                //{
                //    throw new Exception($"Computer failed to pick action, Iteration {errorCont}");
                //}

                Moves move = new Moves();


                // start of better logic

                // for now, stops the player of spamming attack.

                if (computerCreature.health <= (CreatureBase.maxHealth * 0.5f) && computerCreature.energy >= CreatureBase.maxEnergy * 0.2f)
                {
                    action.heal = true;
                    move = Moves.Dodge;
                    inLoop = false;
                }
                else if (computerCreature.health >= (CreatureBase.maxHealth * 0.5f) && computerCreature.energy <= CreatureBase.maxEnergy * 0.2f)
                {
                    action.heal = false;
                    move = Moves.Recharge;
                    inLoop = false;
                }
                else if (computerCreature.health >= (CreatureBase.maxHealth * 0.5f) && computerCreature.energy >= CreatureBase.maxEnergy * 0.5f)
                {
                    action.heal = false;
                    move = Moves.SpecialAttack;
                    inLoop = false;
                }
                else if (computerCreature.health >= (CreatureBase.maxHealth * 0.5f) && computerCreature.energy >= CreatureBase.maxEnergy * 0.2f)
                {
                    action.heal = false;
                    move = Moves.Attack;
                    inLoop = false;
                }


                // end of better logic


                int computerChoice = RougeGameUtil.RandomInt((int)Enum.GetValues(typeof(Moves)).GetValue(0), Enum.GetValues(typeof(Moves)).Length);

                move = RougeGameUtil.ConvertIntIntoMoves(computerChoice);

                if (move == Moves.Heal && !action.heal && computerCreature.health <= (CreatureBase.maxHealth * 0.8f) && computerCreature.energy >= CreatureBase.healCost)
                {
                    action.heal = true;
                }
                //else if (computerChoice == 5 && action.heal)
                //{
                //    // pick again
                //}
                else if (move == Moves.Attack && computerCreature.energy >= CreatureBase.attackCost)
                {
                    action.action = move;
                    inLoop = false;
                }
                else if (move == Moves.SpecialAttack && computerCreature.energy >= CreatureBase.specialAttackCost)
                {
                    action.action = move;
                    inLoop = false;
                }
                else if (move == Moves.Recharge && computerCreature.energy <= (CreatureBase.maxEnergy * 0.8f))
                {
                    action.action = move;
                    inLoop = false;
                }
                else if (move == Moves.Dodge)
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
