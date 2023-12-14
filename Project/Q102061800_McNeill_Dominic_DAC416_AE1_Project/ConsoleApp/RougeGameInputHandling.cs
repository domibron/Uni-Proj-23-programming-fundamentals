using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.Util;
using RougeGame.GameMoves;
using RougeGame.GameCreatures;
using RougeGame.LogSystem;

namespace RougeGame
{
    public class RougeGameInputHandling
    {
#region Player
        public static int HandlePlayerInput(string msg, int minValue, int maxValue, string? forcedInput = null)
        {
            RougeGameUtil.DisplayText(msg);

            string? input = "";

            if (forcedInput == null)
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

        public static GameAction PlayerInput(int playerCreatureEnergy, string displayInfo = "", string? overrideInput = null)
        {
            GameAction returnValue = new GameAction();

            bool waitingForValidInput;

            // overides the loop for testing purposes.
            if (overrideInput == null)
            {
                waitingForValidInput = true;
            } 
            else
            {
                waitingForValidInput  = false;
            }

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


                int value = HandlePlayerInput("\nAction:", minChoice, maxChoice, overrideInput);


                if (value == 0) // I dont want to automatically pick attack. I want the player to pick.
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText($"\nPICK A VALID OPTION!\n", ConsoleColor.Red);
                    continue;
                }

                Moves? move = RougeGameUtil.ConvertIntIntoMoves(value);

                if (move == null)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText($"\nPICK A VALID OPTION!\n", ConsoleColor.Red);
                }
                else if (move == Moves.Heal && !returnValue.heal && playerCreatureEnergy >= healCost)
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
                    returnValue.action = move.Value;
                    waitingForValidInput = false;
                }
                else if (move == Moves.SpecialAttack && playerCreatureEnergy >= specialAttackCost)
                {
                    returnValue.action = move.Value;
                    waitingForValidInput = false;
                }
                else if (move == Moves.Recharge || move == Moves.Dodge)
                {
                    returnValue.action = move.Value;
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

#endregion
        
#region Computer
        // this pervents the AI from dominating the player while staying alive. Just a AI nurf.
        private const int BlunderChance = 50;

        public static GameAction ComputerInput(CreatureBase computerCreature)
        {
            

            GameAction gameAction = new GameAction();

            // 
            int errorCont = 0;

            bool inLoop = true;

            while (inLoop)
            {



                // start of better logic.

                // for now, stops the player of spamming attack.

                int blunder = RougeGameUtil.RandomInt(1, 100);

                if (blunder > BlunderChance)
                {

                    if (computerCreature.health <= (computerCreature.maxHealth * 0.5f) && computerCreature.energy >= (computerCreature.maxEnergy * 0.8f) && computerCreature.energy > 10)
                    {
                        RougeGameLogSystem.Instance.WriteLine("heal");
                        gameAction.heal = true;
                        gameAction.action = Moves.Dodge;
                        inLoop = false;
                        break;
                    }
                    else if (computerCreature.health <= (computerCreature.maxHealth * 0.5f) && computerCreature.energy < (computerCreature.maxEnergy * 0.8f))
                    {
                        RougeGameLogSystem.Instance.WriteLine("recharge 1");
                        gameAction.heal = false;
                        gameAction.action = Moves.Recharge;
                        inLoop = false;
                        break;
                    }
                    else if (computerCreature.health >= (computerCreature.maxHealth * 0.5f) && computerCreature.energy >= computerCreature.maxEnergy * 0.3f && computerCreature.energy > 5)
                    {
                        RougeGameLogSystem.Instance.WriteLine("attack");
                        gameAction.heal = false;
                        gameAction.action = Moves.Attack;
                        inLoop = false;
                        break;
                    }
                }


                // end of better logic.


                // start of old random logic.

                int computerChoice = RougeGameUtil.RandomInt((int)Enum.GetValues(typeof(Moves)).GetValue(0), Enum.GetValues(typeof(Moves)).Length);

                Moves? move = RougeGameUtil.ConvertIntIntoMoves(computerChoice);

                if (move == null)
                {
                    // we want the computer to pick another action.
                    continue;
                }

                if (move == Moves.Heal && !gameAction.heal && computerCreature.health <= (computerCreature.maxHealth * 0.8f) && computerCreature.energy >= computerCreature.healCost)
                {
                    gameAction.heal = true;
                }
                else if (move == Moves.Attack && computerCreature.energy >= computerCreature.attackCost)
                {
                    gameAction.action = move.Value;
                    inLoop = false;
                }
                else if (move == Moves.SpecialAttack && computerCreature.energy >= computerCreature.specialAttackCost)
                {
                    gameAction.action = move.Value;
                    inLoop = false;
                }
                else if (move == Moves.Recharge && computerCreature.energy <= (computerCreature.maxEnergy * 0.8f))
                {
                    gameAction.action = move.Value;
                    inLoop = false;
                }
                else if (move == Moves.Dodge)
                {
                    gameAction.action = move.Value;
                    inLoop = false;
                }
                else
                {
                    // pick again
                    errorCont++;
                    RougeGameUtil.DisplayText("Computer failed", ConsoleColor.Yellow);
                }

                // end of old random logic.
            }

            return gameAction;
        }
#endregion
    }
}
