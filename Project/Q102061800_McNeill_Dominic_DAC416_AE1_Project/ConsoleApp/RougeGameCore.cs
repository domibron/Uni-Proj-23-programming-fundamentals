using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeGame.UI;
using RougeGame.Util;
using RougeGame.SaveSystem;
using RougeGame.FileSystem;
using RougeGame.GameMoves;
using RougeGame.GameCreatures;
using RougeGame.LogSystem;
using RougeGame.Test;
using System.Collections;
using System.Numerics;

namespace RougeGame.Core
{
    public class RougeGameCore
    {

        public static void RunGame()
        {
            // starts the log file
            RougeGameLogSystem.Instance.Initilize();

            // Run tests.
            RougeGameTest.TestAll();

            // initialization.
            RougeGameFileSystem.LoadAllImages();

            // set a new local instance.
            RougeGameSaveManager.instance = new RougeGameSaveManager();
            // Initialize the instance.
            RougeGameSaveManager.instance.Initialize();

            // sets the loop variable.
            bool PlayerEnteredOption = false;


            // start of the game.
            while (!PlayerEnteredOption)
            {
                // clear the console from the debug data or previous game.
                Console.Clear();

                // display title.
                RougeGameUtil.DisplayText("   WELCOME TO\n   ROUGE GAME!");

                // create a new 2D list for the logo.
                List<List<int>> logo = new List<List<int>>();

                // if the logo exists.
                if (RougeGameFileSystem.Images.TryGetValue("logo", out logo))
                {
                    // draw the logo.
                    RougeGameUI.DrawImage(RougeGameFileSystem.Images["logo"]);
                }

                // title for the save data.
                RougeGameUtil.DisplayText("Save Info:");
                // display the save data.
                RougeGameUtil.DisplayText($"Games Played: {RougeGameSaveData.current.rougeGameData.gamesPlayed}\nGames Won: {RougeGameSaveData.current.rougeGameData.gamesWon}\nGames Lost: {RougeGameSaveData.current.rougeGameData.gamesLossed}\nScore: {RougeGameSaveData.current.rougeGameData.score}");
                // create a space.
                RougeGameUtil.DisplayText("");

                // display the title for the menu.
                RougeGameUtil.DisplayText("      MENU");
                // create a space.
                RougeGameUtil.DisplayText("");
                // display options to the player.
                RougeGameUtil.DisplayText("[1] - Play Game\n[2] - Quit\n");

                // get the input from the player and store it.
                string input = Console.ReadLine();

                // the result of the value
                int value;

                // if the input is invalid.
                if(!RougeGameUtil.ValidateInput(input, out value, 1, 2))
                {
                    // you wont see this as the screen is cleared.
                    RougeGameUtil.DisplayText("Enter a valid value!");
                }
                else if (value == 1) 
                {
                    RougeGameLogSystem.Instance.WriteLine("Started Game");
                    // runs the game and saves the result.
                    int gameResult = GameCore();

                    // if the result is 1 then the player won.
                    if (gameResult == 1)
                    {
                        // increment the games played and games won.
                        RougeGameSaveData.current.rougeGameData.gamesPlayed++;
                        // increment the games won.
                        RougeGameSaveData.current.rougeGameData.gamesLossed++;

                        // save the data.
                        RougeGameSaveManager.instance.Save();
                    }
                    // if the result is 2 then the player lost.
                    else if (gameResult == 2)
                    {
                        // increment the games played.
                        RougeGameSaveData.current.rougeGameData.gamesPlayed++;
                        // increment the games lost.
                        RougeGameSaveData.current.rougeGameData.gamesWon++;
                        // add to score.
                        RougeGameSaveData.current.rougeGameData.score += 100;

                        // save the data.
                        RougeGameSaveManager.instance.Save();
                    }
                    RougeGameLogSystem.Instance.WriteLine($"Game ended {gameResult}");

                }
                else if (value == 2)
                {
                    RougeGameLogSystem.Instance.WriteLine($"Player Exited");
                    // save the data.
                    RougeGameSaveManager.instance.Save();
                    // exit the game.
                    Environment.Exit(0);
                }

            }
        }


        public static int GameCore()
        {
            // clear the screen.
            Console.Clear();

            // set loop.
            bool WhilePlayerIsInGame = true;

            // setup creatures.
            CreatureBase Player = new StandardCreature();
            CreatureBase Computer = new StandardCreature();

            while (WhilePlayerIsInGame)
            {

                // display info (TEXT)
                string displayInfo = "";

                displayInfo += RougeGameUtil.PutSpacingAfterString($"Player", 25);
                displayInfo += $"Computer\n";
                displayInfo += RougeGameUtil.PutSpacingAfterString($"HEALTH: [{Player.health}]", 25);
                displayInfo += $"HEALTH: [{Computer.health}]\n";
                displayInfo += RougeGameUtil.PutSpacingAfterString($"ENERGY: [{Player.energy}]", 25);
                displayInfo += $"ENERGY: [{Computer.energy}]\n";

                RougeGameUtil.DisplayText(displayInfo);

                // GUI UI (BARS) will not display if a invalid move or heal because the bars will not update.
                RougeGameUI.DrawUIBars(Player.health, Player.maxHealth, ConsoleColor.Red, Computer.health, Computer.maxHealth, ConsoleColor.DarkRed);
                RougeGameUI.DrawUIBars(Player.energy, Player.maxEnergy, ConsoleColor.Green, Computer.energy, Computer.maxEnergy, ConsoleColor.DarkGreen);

                // game actions for each opponent's move.
                GameAction PlayerAction = new GameAction();
                GameAction ComputerAction = new GameAction();

                // get their move.
                PlayerAction = RougeGameInputHandling.PlayerInput(Player, displayInfo);
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

                // clears the screen so the player can see the game's result.
                Console.Clear();

                // if the both died then its computer win.
                if (Player.health <= 0)
                {
                    // the computer won.
                    RougeGameUtil.DisplayText("You died!");
                    RougeGameUtil.DisplayText("You Lost!", ConsoleColor.Red);

                    RougeGameUtil.DisplayText("\nAny key to continue");
                    Console.ReadLine();

                    WhilePlayerIsInGame = false;

                    return 1;
                }
                else if (Computer.health <= 0)
                {
                    // the player won.
                    RougeGameUtil.DisplayText("Computer died!");
                    RougeGameUtil.DisplayText("You won!", ConsoleColor.Green);

                    RougeGameUtil.DisplayText("\nAny key to continue");
                    Console.ReadLine();

                    WhilePlayerIsInGame = false;

                    return 2;
                }
                else
                {
                    // could put these into a round reset func.
                    Player.RechargeEnergy();
                    Computer.RechargeEnergy();
                    Player.ResetMultipliers();
                    Computer.ResetMultipliers();
                }

                // clean the screen.
                Console.Clear();
            }

            // return if the game errored out.
            return 0;
        }
    }  
}
