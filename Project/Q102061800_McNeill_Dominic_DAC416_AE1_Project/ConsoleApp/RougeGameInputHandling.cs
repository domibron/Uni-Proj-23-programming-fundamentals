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
        // gets the input from the player.
        public static int HandlePlayerInput(string msg, int minValue, int maxValue, string? forcedInput = null)
        {
            // mssage to displayer, can be UI.
            RougeGameUtil.DisplayText(msg);

            // empty input.
            string? input = "";

            // if debugging the game.
            if (forcedInput == null)
            {
                // get input from player.
                input = Console.ReadLine();
            }
            else
            {
                // input is debug input.
                input = forcedInput;
            }

            // value after varifying and converting.
            int value;

            // no, the input should have a value.
            if (RougeGameUtil.ValidateInput(input, out value, minValue, maxValue))
            {
                // return the converted input.
                return value;
            }

            // return 0 as it errored.
            return 0;
        }

        // The main of player input.
        public static GameAction PlayerInput(CreatureBase playerCreature, string displayInfo = "", string? overrideInput = null)
        {
            // the action we are returning.
            GameAction returnGameAction = new GameAction();

            // used to keep in loop.
            bool waitingForValidInput;

            // overides the loop for testing purposes.
            // it will pause testing as it is in a loop that cannot be broken by the testing script.
            if (overrideInput == null)
            {
                waitingForValidInput = true;
            } 
            else
            {
                waitingForValidInput  = false;
            }

            //
            int minChoice = (int)Enum.GetValues(typeof(Moves)).GetValue(0);
            int maxChoice = Enum.GetValues(typeof(Moves)).Length;

            // needed for when we heal, we need to know if we can still do other actions.
            int playerRemaingEnergy = playerCreature.energy;

            while (waitingForValidInput)
            {
                // display move. This is here because the window is cleared so the moves are also cleared.
                if (playerRemaingEnergy >= playerCreature.attackCost)
                {
                    RougeGameUtil.DisplayText("[1] Attack");
                }
                else
                {
                    RougeGameUtil.DisplayText("[1] Attack", ConsoleColor.DarkRed);
                }

                if (playerRemaingEnergy >= playerCreature.specialAttackCost)
                {
                    RougeGameUtil.DisplayText("[2] Special Attack");
                }
                else
                {
                    RougeGameUtil.DisplayText("[2] Special Attack", ConsoleColor.DarkRed);
                }

                RougeGameUtil.DisplayText("[3] Recharge");
                RougeGameUtil.DisplayText("[4] Dodge");

                if (!returnGameAction.heal && playerRemaingEnergy >= playerCreature.healCost)
                {
                    RougeGameUtil.DisplayText("[5] Heal");
                }
                else if (returnGameAction.heal)
                {
                    RougeGameUtil.DisplayText("[5] Heal", ConsoleColor.Blue);
                }
                else
                {
                    RougeGameUtil.DisplayText("[5] Heal", ConsoleColor.DarkRed);
                }

                // end of display.

                // the inputed int move from the player.
                int value = HandlePlayerInput("\nAction:", minChoice, maxChoice, overrideInput);

                // if the value is 0, we should get the player to pick a valid value.
                if (value == 0)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText($"\nPICK A VALID OPTION!\n", ConsoleColor.Red);
                    continue;
                }

                // convert the input into a move.
                Moves? move = RougeGameUtil.ConvertIntIntoMoves(value);

                // if it is invalid then get the player to enter a valid value.
                if (move == null)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText($"\nPICK A VALID OPTION!\n", ConsoleColor.Red);
                }
                // if the player selects heal for the first time this round and have the required energy.
                else if (move == Moves.Heal && !returnGameAction.heal && playerRemaingEnergy >= playerCreature.healCost)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    returnGameAction.heal = true;

                    // set the energy so we know how much we have left for other moves.
                    playerRemaingEnergy -= playerCreature.healCost;
                    playerRemaingEnergy = (int)(playerRemaingEnergy * playerCreature.energyConvertion);
                }
                // if the player selects heal after selecting heal.
                else if (move == Moves.Heal && returnGameAction.heal)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText("\nYOU ALREADY CHOSEN HEAL, BUT YOU CAN SELECT ANOTHER MOVE\n", ConsoleColor.Red);
                }
                // if the player pucked attack and have the required energy.
                else if (move == Moves.Attack && playerRemaingEnergy >= playerCreature.attackCost)
                {
                    returnGameAction.action = move.Value;
                    waitingForValidInput = false;
                }
                // if the player picked special attack and have the required energy.
                else if (move == Moves.SpecialAttack && playerRemaingEnergy >= playerCreature.specialAttackCost)
                {
                    returnGameAction.action = move.Value;
                    waitingForValidInput = false;
                }
                // if the player picked recharge or dodge.
                else if (move == Moves.Recharge || move == Moves.Dodge)
                {
                    returnGameAction.action = move.Value;
                    waitingForValidInput = false;
                }
                // if any condition is not met then get the player to enter a valid value.
                else
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText(displayInfo);
                    RougeGameUtil.DisplayText($"\nPICK A VALID OPTION!\n", ConsoleColor.Red);
                }

            }

            // return the action player player picked.
            return returnGameAction;
        }

#endregion
        
#region Computer
        // this pervents the AI from dominating the player while staying alive. Just a AI nurf. Yes it is very good if this is 0.
        private const int BlunderChance = 50;


        public static GameAction ComputerInput(CreatureBase computerCreature)
        {
            
            // the return action.
            GameAction gameAction = new GameAction();

            // used to see if the AI is picking a invalid move, and how often.
            int errorCont = 0;

            // keeps the loop going until a valid action is chosen.
            bool whileComputerPicksAction = true;

            // how likly for the ai to do a random attack.
            // needs to be outside incase the AI picks a invalid move, it will respin the blunder chance.
            int blunder = RougeGameUtil.RandomInt(1, 100);

            // log it to file.
            RougeGameLogSystem.Instance.WriteLine($"Blunder value: {blunder} > Blunder chance: {BlunderChance}");

            while (whileComputerPicksAction)
            {
                /* start of better logic.    
                 * This is based on data of what i play and others play.
                 * The AI will keep it's health above middle and same for energy.
                 * It will attack if the health and energy is met.
                 * This is very strong.
                 */

                // blunder chance so the ai will not always win / cause a stale mate.
                if (blunder > BlunderChance)
                {
                    // if the health is low and the energy is high then heal.
                    if (computerCreature.health <= (computerCreature.maxHealth * 0.5f) && computerCreature.energy >= (computerCreature.maxEnergy * 0.8f) && computerCreature.energy > computerCreature.attackCost)
                    {
                        RougeGameLogSystem.Instance.WriteLine("heal");

                        gameAction.heal = true;
                        gameAction.action = Moves.Dodge;
                        whileComputerPicksAction = false;
                        break;
                    }
                    // if health is low and energy is low then recharge.
                    else if (computerCreature.health <= (computerCreature.maxHealth * 0.5f) && computerCreature.energy < (computerCreature.maxEnergy * 0.8f))
                    {
                        RougeGameLogSystem.Instance.WriteLine("recharge");

                        gameAction.heal = false;
                        gameAction.action = Moves.Recharge;
                        whileComputerPicksAction = false;
                        break;
                    }
                    // if health is high and energy is suffciant then attack.
                    else if (computerCreature.health >= (computerCreature.maxHealth * 0.5f) && computerCreature.energy >= computerCreature.maxEnergy * 0.3f && computerCreature.energy > computerCreature.attackCost)
                    {
                        RougeGameLogSystem.Instance.WriteLine("attack");

                        gameAction.heal = false;
                        gameAction.action = Moves.Attack;
                        whileComputerPicksAction = false;
                        break;
                    }
                }

                // end of better logic.


                /* start of old random logic.
                 * This is just a random move for the AI to blunder / pick somthing else.
                 * This is required to have a beatable opponent.
                 */

                // pick a random move within the length of the enem. 1 = attack, 2 = special attack, 3 = recharge, 4 = dodge, 5 = heal.
                int computerChoice = RougeGameUtil.RandomInt((int)Enum.GetValues(typeof(Moves)).GetValue(0), Enum.GetValues(typeof(Moves)).Length);

                // convert the choice into a valid move.
                Moves? move = RougeGameUtil.ConvertIntIntoMoves(computerChoice);

                // if the move is null, get the computer to pick another move.
                if (move == null)
                {
                    // we want the computer to pick another action.
                    continue;
                }

                // if the computer picked heal, and not picked again, and have the reqiured energy.
                if (move == Moves.Heal && !gameAction.heal && computerCreature.energy >= computerCreature.healCost)
                {
                    gameAction.heal = true;
                }
                // if the computer picked attack and have the reqiured energy.
                else if (move == Moves.Attack && computerCreature.energy >= computerCreature.attackCost)
                {
                    gameAction.action = move.Value;
                    whileComputerPicksAction = false;
                }
                // if the computer picked special attack and have the required energy.
                else if (move == Moves.SpecialAttack && computerCreature.energy >= computerCreature.specialAttackCost)
                {
                    gameAction.action = move.Value;
                    whileComputerPicksAction = false;
                }
                // if the computer picked recharge.
                else if (move == Moves.Recharge)
                {
                    gameAction.action = move.Value;
                    whileComputerPicksAction = false;
                }
                // if the computer picked dodge.
                else if (move == Moves.Dodge)
                {
                    gameAction.action = move.Value;
                    whileComputerPicksAction = false;
                }
                // else if no codition is met
                else
                {
                    // pick again
                    errorCont++;
                    RougeGameLogSystem.Instance.WriteLine($"Computer failed Count: {errorCont}");
                }

                // end of old random logic.
            }

            // return the game acrtion.
            return gameAction;
        }
#endregion
    }
}
