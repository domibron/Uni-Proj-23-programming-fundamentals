using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.Util;
using RougeGame.GameMoves;
using RougeGame.GameCreatures;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace RougeGame
{
    public class RougeGameInputHandling
    {
        // a bit of spice. Using this so automatic testing can be carried out.
        // using my own version of read line so i can 
        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("browser")]
        public static string? ReadLine(string input = "")
        {
            if (string.IsNullOrEmpty(input))
            {
                return Console.In.ReadLine();
            }
            else
            {
                return input;
            }
        }

        public static int HandlePlayerInput(string msg, int minValue, int maxValue, string forcedInput = "")
        {
            RougeGameUtil.DisplayText(msg);

            string? input = "";

            input = ReadLine(forcedInput);

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
                // display move. This is here because the window is cleared so the moves are also cleared.
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

                // end of display.


                int value = HandlePlayerInput("\nAction:", minChoice, maxChoice);


                if (value == 0) // I dont want to automattically pick attack. I want the player to pick.
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText($"\nPICK A VALID OPTION!\n", ConsoleColor.Red);
                    continue;
                }

                Moves move = RougeGameUtil.ConvertIntIntoMoves(value);

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
                // not really, the player can still win.

                if (computerCreature.health <= (computerCreature.maxHealth * 0.5f) && computerCreature.energy >= computerCreature.maxEnergy * 0.2f && computerCreature.energy > 10)
                {
                    action.heal = true;
                    move = Moves.Dodge;
                    inLoop = false;
                    break;
                }
                else if (computerCreature.health >= (computerCreature.maxHealth * 0.5f) && computerCreature.energy <= computerCreature.maxEnergy * 0.2f && computerCreature.energy > 20)
                {
                    action.heal = false;
                    move = Moves.Recharge;
                    inLoop = false;
                    break;
                }
                else if (computerCreature.health >= (computerCreature.maxHealth * 0.5f) && computerCreature.energy >= computerCreature.maxEnergy * 0.3f && computerCreature.energy > 5)
                {
                    action.heal = false;
                    move = Moves.Attack;
                    inLoop = false;
                    break;
                }


                // end of better logic


                int computerChoice = RougeGameUtil.RandomInt((int)Enum.GetValues(typeof(Moves)).GetValue(0), Enum.GetValues(typeof(Moves)).Length);

                move = RougeGameUtil.ConvertIntIntoMoves(computerChoice);

                if (move == Moves.Heal && !action.heal && computerCreature.health <= (computerCreature.maxHealth * 0.8f) && computerCreature.energy >= computerCreature.healCost)
                {
                    action.heal = true;
                }
                //else if (computerChoice == 5 && action.heal)
                //{
                //    // pick again
                //}
                else if (move == Moves.Attack && computerCreature.energy >= computerCreature.attackCost)
                {
                    action.action = move;
                    inLoop = false;
                }
                else if (move == Moves.SpecialAttack && computerCreature.energy >= computerCreature.specialAttackCost)
                {
                    action.action = move;
                    inLoop = false;
                }
                else if (move == Moves.Recharge && computerCreature.energy <= (computerCreature.maxEnergy * 0.8f))
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
