﻿using System;

namespace GameOfLife
{
    //Creates and draws all UI elements
    public class UIElements
    {
        GameSave gameSave = new GameSave();

        //Creates the start menu
        public void StartMenu()
        {
            Console.WriteLine("Hello to Game of Life!");
            Console.WriteLine(" ");
            Console.WriteLine("Choose the size of field, by typing a number:");
            Console.WriteLine("1. 30 X 60");
            Console.WriteLine("2. 40 X 80");
            Console.WriteLine("3. Custom test grid");
            Console.WriteLine("4. Load last saved game");
            Console.WriteLine("5. Exit");

            string menuNuber = Console.ReadLine();
            Grid grid = new Grid();
            GameSave gameSave = new GameSave();

            switch (menuNuber)
            {
                case "1":
                    grid.CreateGrid(30, 60);
                    break;
                case "2":
                    grid.CreateGrid(40, 80);
                    break;
                case "3":
                    grid.CreateCustomGrid(30, 30);
                    break;
                case "4":
                    gameSave.LoadSave();
                    break;
                case "5":
                    break;
            }
        }

        //Checks if Game of Life needs to be: paused, unpaused or saved
        public void CheckForPauseOrSave(bool[,] gameGrid, int iteration, int aliveCellCount) 
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
                        gameSave.SaveGame(gameGrid, iteration, aliveCellCount);
                        Console.WriteLine("Game Saved!");
                    }
                } while (true);
            }
        }

        //Draws the grid every time it has been updated
        public void ShowGrid(bool[,] gameGrid, int iteration, int aliveCellCount, int height, int width)
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("Iteration: " + iteration);
            Console.WriteLine("Alive cell count: " + aliveCellCount);
            Console.WriteLine("");
            Console.WriteLine("Press Space to pause and unpause");
            Console.WriteLine("While Paused press S to save");
            Console.WriteLine("");

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (gameGrid[i, j] == true)
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
    }
}