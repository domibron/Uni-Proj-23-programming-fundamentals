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

            const int minChoice = 1;
            const int maxChoice = 5;

            const int attackCost = 5;
            const int specialAttackCost = 20;
            const int healCost = 10;

            while (inLoop)
            {
                if (energyCalc >= healCost)
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

                if (!returnValue.heal && energyCalc >= attackCost)
                {
                    RougeGameUtil.DisplayText("[5] Heal");
                }
                else
                {
                    RougeGameUtil.DisplayText("[5] Heal", ConsoleColor.DarkRed);
                }


                int value = RougeGameCore.HandlePlayerInput("\nAction:", minChoice, maxChoice);

                Moves move = RougeGameUtil.ConvertIntIntoMoves(value);
                  

                // this hurts my eyes! AH!

                /* I think maybe redo / make it more readable
                 * Can any of this go into functions.
                 * also relook at the flow chart
                 */
                if (move == Moves.Heal && !returnValue.heal && energyCalc >= healCost)
                {
                    Console.Clear();
                    returnValue.heal = true;
                    energyCalc /= 2;
                }
                else if (move == Moves.Heal && returnValue.heal)
                {
                    Console.Clear();
                    RougeGameUtil.DisplayText("\nYOU ALREADY CHOSEN HEAL, YOU CAN PICK ANOTHER MOVE\n", ConsoleColor.Red);
                }
                else if (move == Moves.Attack && energyCalc >= attackCost)
                {
                    returnValue.action = move;
                    inLoop = false;
                }
                else if (move == Moves.SpecialAttack && energyCalc >= specialAttackCost)
                {
                    returnValue.action = move;
                    inLoop = false;
                }
                else if (move == Moves.Recharge || move == Moves.Dodge)
                {
                    returnValue.action = move;
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

        public static int GameCore()
        {
            bool inLoop = true;

            Creature Player = new Creature();
            Creature Computer = new Creature();

            const int healCost = 10;
            const int rechargeAmmount = 4;

            const int attackCost = 5;
            const int attackChanceToHit = 80;
            const int attackMinDamage = 1;
            const int attackMaxDamage = 10;
            
            const int specialAttackCost = 20;
            const int specialAttackChanceToHit = 50;
            const int specialAttackMinDamage = 5;
            const int specialAttackMaxDamage = 20;

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
                if (PlayerAction.action == Moves.Recharge)
                {
                    RougeGameMoves.Recharge(ref Player, ref Computer);
                }

                if (PlayerAction.action == Moves.Dodge)
                {
                    RougeGameMoves.Dodge(ref Player, ref Computer);
                }

                // computer 3 and 4.
                if (ComputerAction.action == Moves.Recharge)
                {
                    RougeGameMoves.Recharge(ref Computer, ref Player);
                }

                if (ComputerAction.action == Moves.Dodge)
                {
                    RougeGameMoves.Dodge(ref Computer, ref Player);
                }

                // player 1 and 2
                if (PlayerAction.action == Moves.Attack)
                {
                    RougeGameMoves.Attack(ref Player, ref Computer, attackChanceToHit, attackMinDamage, attackMaxDamage, attackCost);
                }

                if (PlayerAction.action == Moves.SpecialAttack)
                {
                    RougeGameMoves.Attack(ref Player, ref Computer, specialAttackChanceToHit, specialAttackMinDamage, specialAttackMaxDamage, specialAttackCost);
                }

                // computer 1 and 2
                if (ComputerAction.action == Moves.Attack)
                {
                    RougeGameMoves.Attack(ref Computer, ref Player, attackChanceToHit, attackMinDamage, attackMaxDamage, attackCost);
                }

                if (ComputerAction.action == Moves.SpecialAttack)
                {
                    RougeGameMoves.Attack(ref Computer, ref Player, specialAttackChanceToHit, specialAttackMinDamage, specialAttackMaxDamage, specialAttackCost);
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
                    RougeGameMoves.EnergyRecharge(ref Player, rechargeAmmount);
                    RougeGameMoves.EnergyRecharge(ref Computer, rechargeAmmount);

                    RougeGameMoves.ResetMults(ref Player);
                    RougeGameMoves.ResetMults(ref Computer);
                }

                

                Console.Clear();
                

            }

            return 0;
        }
    }

    

    
}
