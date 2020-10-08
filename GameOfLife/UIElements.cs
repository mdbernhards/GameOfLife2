﻿using System;
using System.Linq;

namespace GameOfLife
{
    /// <summary>
    /// Creates and draws all UI elements
    /// </summary>
    public class UIElements
    {
        GameSave gameSave = new GameSave();

        /// <summary>
        /// Creates the start menu
        /// </summary>
        public void StartMenu()
        {
            Grid grid = new Grid();
            GameSave gameSave = new GameSave();

            Console.WriteLine("Hello to Game of Life!");
            Console.WriteLine(" ");
            Console.WriteLine("Choose the size of field, by typing a number:");
            Console.WriteLine("1. 30 X 60");
            Console.WriteLine("2. 40 X 80");
            Console.WriteLine("3. Custom test grid");
            Console.WriteLine("4. 1000 games in paralel");
            Console.WriteLine("5. Load last saved game");
            Console.WriteLine("6. Exit");

            string menuNuber = Console.ReadLine();

            switch (menuNuber)
            {
                case "1":
                    grid.CreateGrids(30, 60, 1); //height, width, number of games in paralel
                    break;
                case "2":
                    grid.CreateGrids(40, 80, 1);
                    break;
                case "3":
                    grid.CreateCustomGrid(30, 30);
                    break;
                case "4":
                    grid.CreateGrids(18, 40, 1000);
                    break;
                case "5":
                    var saveInfo = gameSave.ReadSaveFile();
                    grid.CreateGridFromFile(saveInfo.Item1, saveInfo.Item2, saveInfo.Item3);
                    break;
                case "6":
                    break;
                default:
                    Console.Clear();
                    StartMenu();
                    break;
            }
        }

        /// <summary>
        /// Checks if Game of Life needs to be: paused, unpaused or saved
        /// </summary>
        public void CheckForPauseOrSave(bool[, ,] gameGrid, int iteration, int[] aliveCellCount, int numberOfGames) 
        {
            while (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                Console.WriteLine("Game Paused!");

                do
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.Spacebar)
                    {
                        break;
                    }

                    if (key == ConsoleKey.S)
                    {
                        gameSave.SaveGame(gameGrid, iteration, aliveCellCount, numberOfGames);
                        Console.WriteLine("Game Saved!");
                    }
                } while (true);
            }
        }

        /// <summary>
        /// Draws the grid every time it has been updated
        /// </summary>
        public void DrawGrid(bool[, ,] gameGrid, int iteration, int aliveCellCount, int height, int width, int aliveGridCount)
        {
            Console.Clear();

            Console.WriteLine("Iteration: " + iteration);
            Console.WriteLine("Alive cell count: " + aliveCellCount);
            Console.WriteLine("Alive Grid count: " + aliveGridCount);
            Console.WriteLine("");
            Console.WriteLine("Press Space to pause and unpause");
            Console.WriteLine("While Paused press S to save");
            Console.WriteLine("");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (gameGrid[i, j, 0] == true)
                    {
                        Console.Write("█");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Draws eight grids every time they have been updated
        /// </summary>
        public void DrawEightGrids(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int height, int width, int[] selectedGames, int aliveGridCount)
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("Iteration: " + iteration);
            Console.WriteLine("Total alive cell count: " + aliveCellCount.Sum());
            Console.WriteLine("Alive Grid count: " + aliveGridCount);
            Console.WriteLine("");
            Console.WriteLine("Press Space to pause and unpause");
            Console.WriteLine("While Paused press S to save");
            Console.WriteLine("");

            for (int l = 0; l < 2; l++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (l == 0)
                    {
                        Console.Write("Game: " + (selectedGames[i] + 1)  + "  Alive cell count: " + aliveCellCount[selectedGames[i]] + new string(' ', gameGrid.GetLength(1) - 25));
                    }
                    else
                    {
                        Console.Write("Game: " + (selectedGames[i + 4] + 1) + "  Alive cell count: " + aliveCellCount[selectedGames[i + 4]] + new string(' ', gameGrid.GetLength(1) - 25));
                    }
                }

                Console.WriteLine("");

                for (int i = 0; i < height; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        if (l == 1)
                        {
                            k += 4;
                        }

                        for (int j = 0; j < width; j++)
                        {
                            if (gameGrid[i, j, selectedGames[k]] == true)
                            {
                                Console.Write("█");
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }

                        if (l == 1)
                        {
                            k -= 4;
                        }

                        Console.Write(" || ");
                    }

                    Console.WriteLine("");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// UI that lets you select which grids to show, returns the grid "id's"
        /// </summary>
        public int[] GameSelection(int numberOfGames)
        {
            int[] selectedGames = new int[8];

            Console.Clear();
            Console.WriteLine("Choose Games to show from 1 to " + numberOfGames);

            for (int i = 0; i < 8; i++)
            {
                Console.Write("Game No. " + (i + 1) + ": ");

                string number = Console.ReadLine();
                int numberValue;
                if (int.TryParse(number, out numberValue))
                {
                    if (numberValue >= 1 && numberValue <= 1000)
                    {
                        selectedGames[i] = numberValue - 1;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }

            return selectedGames;
        }
    }
}