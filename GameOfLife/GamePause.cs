﻿using System;
using System.Timers;
using System.Text;

namespace GameOfLife
{
    /// <summary>
    /// Class for pausing and saving
    /// </summary>
    public class GamePause
    {
        private Menus menu;

        public GamePause()
        {
            menu = new Menus();
        }

        /// <summary>
        /// Checks if Game of Life needs to be: paused, unpaused or saved
        /// </summary>
        public void CheckForPauseOrSave(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount, Timer timer)
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
                        GameSave.SaveGame(gameGrid, iteration, aliveCellCount, aliveGridCount);
                        menu.DisplayOutput("Game Saved!");
                    }
                } while (true);
            }
        }
    }
}