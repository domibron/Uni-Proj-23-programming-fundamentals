using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RougeGame
{
    public class RougeGameCore
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

        public static GameAction PlayerInput(Creature playerCreature)
        {
            GameAction returnValue = new GameAction();

            bool inLoop = true;
            int energyCalc = playerCreature.energy;

            while (inLoop)
            {
                if (energyCalc >= 10)
                {
                    RougeGameUtil.DisplayText("[1] Attack");
                }
                else
                {
                    RougeGameUtil.DisplayText("[1] Attack", ConsoleColor.DarkRed);
                }

                if (energyCalc >= 20)
                {
                    RougeGameUtil.DisplayText("[2] Special Attack");
                }
                else
                {
                    RougeGameUtil.DisplayText("[2] Special Attack", ConsoleColor.DarkRed);
                }

                RougeGameUtil.DisplayText("[3] Recharge");
                RougeGameUtil.DisplayText("[4] Dodge");

                if (!returnValue.heal && energyCalc >= 10)
                {
                    RougeGameUtil.DisplayText("[5] Heal");
                }
                else
                {
                    RougeGameUtil.DisplayText("[5] Heal", ConsoleColor.DarkRed);
                }


                int value = RougeGameCore.HandlePlayerInput("\nAction:", 1, 5);

                  

                // this hurts my eyes! AH!

                /* I think maybe redo / make it more readable
                 * Can any of this go into functions.
                 * also relook at the flow chart
                 */
                if (value == 5 && !returnValue.heal && energyCalc >= 10)
                {
                    Console.Clear();
                    returnValue.heal = true;
                    energyCalc /= 2;
                }
                else if (value == 5 && returnValue.heal)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText("\nYOU ALREADY CHOSEN HEAL, YOU CAN PICK ANOTHER MOVE\n", ConsoleColor.Red);
                }
                else if (value == 1 && energyCalc >= 10)
                {
                    returnValue.intAction = value;
                    inLoop = false;
                }
                else if (value == 2 && energyCalc >= 20)
                {
                    returnValue.intAction = value;
                    inLoop = false;
                }
                else if (value == 3 || value == 4)
                {
                    returnValue.intAction = value;
                    inLoop = false;
                }
                else
                {
                    Console.Clear();
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

            while (inLoop)
            {
                if (errorCont == 5)
                {
                    throw new Exception($"Computer failed to pick action, Iteration {errorCont}");
                }

                int computerChoice = RougeGameUtil.RandomInt(1, 5);

                if (computerChoice == 5 && !action.heal && computerCreature.health <= 80 && computerCreature.energy >= 2)
                {
                    action.heal = true;
                }
                else if (computerChoice == 5 && action.heal)
                {
                    // pick again
                }
                else if (computerChoice == 1 && computerCreature.energy >= 10)
                {
                    action.intAction = computerChoice;
                    inLoop = false;
                }
                else if (computerChoice == 2 && computerCreature.energy >= 20)
                {
                    action.intAction = computerChoice;
                    inLoop = false;
                }
                else if (computerChoice == 3 || computerChoice == 4)
                {
                    action.intAction = computerChoice;
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

        public static int GameCore()
        {
            bool inLoop = true;

            Creature Player = new Creature();
            Creature Computer = new Creature();

            const int healCost = 10;
            const int RechargeAmmount = 4;

            const int CostAttack = 5;
            const int CostSpecialAttack = 20;


            while (inLoop)
            {
                // == move elsewhere ==
                //// a way to leave the game / quit.
                //if (RougeGameCore.HandlePlayerInput("Quit Game\n1 - yes  |  2 - no", 1, 2) == 1)
                //{
                //    inLoop = false;
                //}

                // display info
                RougeGameUtil.DisplayText($"Player\nHEALTH: [{Player.health}]\nENERGY: [{Player.energy}]\n");
                RougeGameUtil.DisplayText($"Computer\nHEALTH: [{Computer.health}]\nENERGY: [{Computer.energy}]\n");

                GameAction PlayerAction = new GameAction();
                GameAction ComputerAction = new GameAction();

                PlayerAction = RougeGameCore.PlayerInput(Player);
                ComputerAction = RougeGameCore.ComputerInput(Computer);

                // player heal.
                if (PlayerAction.heal)
                {
                    RougeGameMoves.Heal(ref Player, healCost);
                }

                // computer heal.
                if (ComputerAction.heal)
                {
                    RougeGameMoves.Heal(ref Computer, healCost);
                }

                // player 3 and 4.
                if (PlayerAction.intAction == 3)
                {
                    RougeGameMoves.Recharge(ref Player, ref Computer);
                }

                if (PlayerAction.intAction == 4)
                {
                    RougeGameMoves.Dodge(ref Player, ref Computer);
                }

                // computer 3 and 4.
                if (ComputerAction.intAction == 3)
                {
                    RougeGameMoves.Recharge(ref Computer, ref Player);
                }

                if (ComputerAction.intAction == 4)
                {
                    RougeGameMoves.Dodge(ref Computer, ref Player);
                }

                // player 1 and 2
                if (PlayerAction.intAction == 1)
                {
                    RougeGameMoves.Attack(ref Player, ref Computer);
                }

                if (PlayerAction.intAction == 2)
                {
                    RougeGameMoves.Attack(ref Player, ref Computer, 50, 5, 20, 20);
                }

                // computer 1 and 2
                if (ComputerAction.intAction == 1)
                {
                    RougeGameMoves.Attack(ref Computer, ref Player);
                }

                if (ComputerAction.intAction == 2)
                {
                    RougeGameMoves.Attack(ref Computer, ref Player, 50, 5, 20, 20);
                }

                Console.Clear();

                if (Player.health <= 0)
                {
                    RougeGameUtil.DisplayText("You died!");
                    RougeGameUtil.DisplayText("You Lost!", ConsoleColor.Red);
                    //return 1;

                    RougeGameUtil.DisplayText("\nAny key to continue");
                    Console.ReadLine();

                    Player = new Creature();
                    Computer = new Creature();
                }
                else if (Computer.health <= 0)
                {
                    RougeGameUtil.DisplayText("Computer died!");
                    RougeGameUtil.DisplayText("You won!", ConsoleColor.Green);
                    //return 2;

                    RougeGameUtil.DisplayText("\nAny key to continue");
                    Console.ReadLine();

                    Player = new Creature();
                    Computer = new Creature();
                }
                else
                {
                    RougeGameMoves.EnergyRecharge(ref Player, RechargeAmmount);
                    RougeGameMoves.EnergyRecharge(ref Computer, RechargeAmmount);

                    RougeGameMoves.ResetMults(ref Player);
                    RougeGameMoves.ResetMults(ref Computer);
                }

                

                Console.Clear();
                

            }

            return 0;
        }
    }

    

    
}
