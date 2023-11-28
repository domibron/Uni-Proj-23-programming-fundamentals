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

            CreatureBase Player = new CreatureBase();
            CreatureBase Computer = new CreatureBase();

            while (inLoop)
            {
                // == move elsewhere ==
                //// a way to leave the game / quit.
                //if (RougeGameCore.HandlePlayerInput("Quit Game\n1 - yes  |  2 - no", 1, 2) == 1)
                //{
                //    inLoop = false;
                //}

                // display info
                string displayInfo = "";

                displayInfo += RougeGameUtil.PutSpacingInString($"Player", 25);
                displayInfo += $"Computer\n";
                displayInfo += RougeGameUtil.PutSpacingInString($"HEALTH: [{Player.health}]", 25);
                displayInfo += $"HEALTH: [{Computer.health}]\n";
                displayInfo += RougeGameUtil.PutSpacingInString($"ENERGY: [{Player.energy}]", 25);
                displayInfo += $"ENERGY: [{Computer.energy}]\n";

                RougeGameUtil.DisplayText(displayInfo);

                RougeGameUI.DrawUIBars(Player, Computer);


                GameAction PlayerAction = new GameAction();
                GameAction ComputerAction = new GameAction();

                PlayerAction = RougeGameInputHandling.PlayerInput(Player.energy, displayInfo);
                ComputerAction = RougeGameInputHandling.ComputerInput(Computer);

                // player heal.
                if (PlayerAction.heal)
                {
                    RougeGameMoves.Heal(ref Player);
                }

                // computer heal.
                if (ComputerAction.heal)
                {
                    RougeGameMoves.Heal(ref Computer);
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
                    RougeGameMoves.Attack(ref Player, ref Computer);
                }

                if (PlayerAction.action == Moves.SpecialAttack)
                {
                    RougeGameMoves.SpecialAttack(ref Player, ref Computer);
                }

                // computer 1 and 2
                if (ComputerAction.action == Moves.Attack)
                {
                    RougeGameMoves.Attack(ref Computer, ref Player);
                }

                if (ComputerAction.action == Moves.SpecialAttack)
                {
                    RougeGameMoves.SpecialAttack(ref Computer, ref Player);
                }

                Console.Clear();

                if (Player.health <= 0)
                {
                    RougeGameUtil.DisplayText("You died!");
                    RougeGameUtil.DisplayText("You Lost!", ConsoleColor.Red);
                    //return 1;

                    RougeGameUtil.DisplayText("\nAny key to continue");
                    Console.ReadLine();

                    Player.NewCreature();
                    Computer.NewCreature();
                }
                else if (Computer.health <= 0)
                {
                    RougeGameUtil.DisplayText("Computer died!");
                    RougeGameUtil.DisplayText("You won!", ConsoleColor.Green);
                    //return 2;

                    RougeGameUtil.DisplayText("\nAny key to continue");
                    Console.ReadLine();

                    Player.NewCreature();
                    Computer.NewCreature();
                }
                else
                {
                    // could put these into a round reset func.
                    RougeGameMoves.EnergyRechargeForRound(ref Player, ref Computer);

                    RougeGameMoves.ResetMults(ref Player, ref Computer);
                }

                Console.Clear();
            }

            return 0;
        }
    }

    

    
}
