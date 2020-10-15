using GameOfLife.Interfaces;
using System;
using System.Timers;

namespace GameOfLife
{
    /// <summary>
    /// Class for pausing and saving
    /// </summary>
    public class GamePause : IGamePause
    {
        private IMenus menu;
        private GameSave gameSave;

        /// <summary>
        /// Constuctor  for pausing and saving class
        /// </summary>
        public GamePause()
        {
            menu = new Menus();
            gameSave = new GameSave();
        }

        /// <summary>
        /// Checks if Game of Life needs to be: paused, unpaused or saved
        /// </summary>
        /// <param name="game"> Game object that stores information about game grid</param>
        /// <param name="timer"> Timer object that can be stoped and started up again</param>
        public void CheckForPauseOrSave(Games game, Timer timer)
        {
            while (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                timer.Stop();
                menu.DisplayOutput("Game Paused!");

                do
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.Spacebar)
                    {
                        timer.Start();
                        break;
                    }

                    if (key == ConsoleKey.S)
                    {
                        gameSave.SaveGame(game);
                        menu.DisplayOutput("Game Saved!");
                    }
                } while (true);
            }
        }
    }
}