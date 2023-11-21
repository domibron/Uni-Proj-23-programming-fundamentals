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
        

        public static int GameCore()
        {
            bool inLoop = true;

            //Creature Player = new Creature();
            //Creature Computer = new Creature();

            CreatureBase Player = new CreatureBase();
            CreatureBase Computer = new CreatureBase();

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
                //string displayInfo = $"Player\nHEALTH: [{Player.health}]\nENERGY: [{Player.energy}]\n\nComputer\nHEALTH: [{Computer.health}]\nENERGY: [{Computer.energy}]\n";

                string displayInfo = "";

                displayInfo += RougeGameUtil.PutSpacingInString($"Player", 25);
                displayInfo += $"Computer\n";
                displayInfo += RougeGameUtil.PutSpacingInString($"HEALTH: [{Player.health}]", 25);
                displayInfo += $"HEALTH: [{Computer.health}]\n";
                displayInfo += RougeGameUtil.PutSpacingInString($"ENERGY: [{Player.energy}]", 25);
                displayInfo += $"ENERGY: [{Computer.energy}]\n";

                RougeGameUtil.DisplayText(displayInfo);
                //RougeGameUtil.DisplayText($"\n\n\n");
                


                GameAction PlayerAction = new GameAction();
                GameAction ComputerAction = new GameAction();

                PlayerAction = RougeGameInputHandling.PlayerInput(Player, displayInfo);
                ComputerAction = RougeGameInputHandling.ComputerInput(Computer);

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
                    RougeGameMoves.EnergyRechargeForRound(ref Player, rechargeAmmount);
                    RougeGameMoves.EnergyRechargeForRound(ref Computer, rechargeAmmount);

                    RougeGameMoves.ResetMults(ref Player);
                    RougeGameMoves.ResetMults(ref Computer);
                }

                Console.Clear();
            }

            return 0;
        }
    }

    

    
}
