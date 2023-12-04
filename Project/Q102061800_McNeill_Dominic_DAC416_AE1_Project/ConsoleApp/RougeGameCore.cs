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

        public static void RunGame()
        {
            RougeGameFileSystem.LoadAllImages();

            bool PlayerEnteredOption = false;

            while (!PlayerEnteredOption) { 

                Console.Clear();

                RougeGameUtil.DisplayText("   WELCOME TO\n   ROUGE GAME!");

                List<List<int>> logo = new List<List<int>>();

                if (RougeGameFileSystem.Images.TryGetValue("logo", out logo))
                    RougeGameUI.DrawImage(RougeGameFileSystem.Images["logo"]);

                RougeGameUtil.DisplayText("");
                RougeGameUtil.DisplayText("      MENU");
                RougeGameUtil.DisplayText("");
                RougeGameUtil.DisplayText("[1] - Play Game\n[2] - Quit\n");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    GameCore();
                }
                else if (input == "2")
                {
                    Environment.Exit(0);
                }

            }
        }


        public static int GameCore()
        {
            Console.Clear();
            bool WhilePlayerIsInGame = true;

            CreatureBase Player = new StandardCreature();
            CreatureBase Computer = new StandardCreature();

            while (WhilePlayerIsInGame)
            {

                // display info (TEXT)
                string displayInfo = "";

                displayInfo += RougeGameUtil.PutSpacingInString($"Player", 25);
                displayInfo += $"Computer\n";
                displayInfo += RougeGameUtil.PutSpacingInString($"HEALTH: [{Player.health}]", 25);
                displayInfo += $"HEALTH: [{Computer.health}]\n";
                displayInfo += RougeGameUtil.PutSpacingInString($"ENERGY: [{Player.energy}]", 25);
                displayInfo += $"ENERGY: [{Computer.energy}]\n";

                RougeGameUtil.DisplayText(displayInfo);

                // GUI UI
                RougeGameUI.DrawUIBars(Player.health, Player.maxHealth, ConsoleColor.Red, Computer.health, Computer.maxHealth, ConsoleColor.DarkRed);
                RougeGameUI.DrawUIBars(Player.energy, Player.maxEnergy, ConsoleColor.Green, Computer.energy, Computer.maxEnergy, ConsoleColor.DarkGreen);


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
                    RougeGameMoves.SpecialAttack(Player, Computer);
                }

                // computer 1 and 2
                if (ComputerAction.action == Moves.Attack)
                {
                    RougeGameMoves.Attack(ref Computer, ref Player);
                }

                if (ComputerAction.action == Moves.SpecialAttack)
                {
                    RougeGameMoves.SpecialAttack(Computer, Player);
                }

                Console.Clear();

                if (Player.health <= 0)
                {
                    RougeGameUtil.DisplayText("You died!");
                    RougeGameUtil.DisplayText("You Lost!", ConsoleColor.Red);
                    //return 1;

                    RougeGameUtil.DisplayText("\nAny key to continue");
                    Console.ReadLine();

                    WhilePlayerIsInGame = false;

                    //Player.NewCreature();
                    //Computer.NewCreature();
                }
                else if (Computer.health <= 0)
                {
                    RougeGameUtil.DisplayText("Computer died!");
                    RougeGameUtil.DisplayText("You won!", ConsoleColor.Green);
                    //return 2;

                    RougeGameUtil.DisplayText("\nAny key to continue");
                    Console.ReadLine();

                    WhilePlayerIsInGame = false;

                    //Player.NewCreature();
                    //Computer.NewCreature();
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
