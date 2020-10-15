﻿using GameOfLife.Interfaces;
using System;
using System.Linq;
using System.Timers;

namespace GameOfLife
{
    /// <summary>
    /// Handles Game of Life logic
    /// </summary>
    public class Grid
    {
        public Games Game { get; set; }

        private bool[,,] NextGameGrid;
        private bool FreshGame;
        private int[] LastAliveCellCount;
        private int[] SelectedGames;
        private int NumberOfGames;
        private int FreshAliveGridCount;

        private static Timer UpdateTimer;
        private IMenus Menu;
        private IGamePause Pause;

        private const bool AliveCell = true;
        private const bool DeadCell = false;

        /// <summary>
        /// Constructor to prepair Game of Life loaded from file
        /// </summary>
        public Grid(Games saveInfo)
        {
            Menu = new Menus();
            Pause = new GamePause();
            Game = saveInfo;

            NumberOfGames = Game.GameGrid.GetLength(2);
            LastAliveCellCount = new int[NumberOfGames];
            SelectedGames = new int[8];
            FreshGame = true;

            NextGameGrid = new bool[Game.GameGrid.GetLength(0), Game.GameGrid.GetLength(1), NumberOfGames];

            FreshAliveGridCount = Game.AliveGridCount;

            Array.Copy(Game.AliveCellCount, LastAliveCellCount, Game.AliveCellCount.Length);
            Array.Copy(Game.GameGrid, NextGameGrid, Game.GameGrid.Length);
        }

        /// <summary>
        /// Constructor to prepair a new and fresh Game of Life
        /// </summary>
        public Grid(int height, int width, int numberOfGames)
        {
            Menu = new Menus();
            Pause = new GamePause();
            Game = new Games(new bool[height, width, numberOfGames], 0, new int[numberOfGames], numberOfGames);
            NumberOfGames = numberOfGames;

            SelectedGames = new int[8];
            NextGameGrid = new bool[Game.Height, Game.Width, NumberOfGames];
            LastAliveCellCount = new int[NumberOfGames];
        }

        /// <summary>
        /// Constructor for unit tests
        /// </summary>
        public Grid(IMenus menu, IGamePause gamePause, int numberOfGames)
        {
            Menu = menu;
            Pause = gamePause;
            NumberOfGames = numberOfGames;
        }

        /// <summary>
        /// Creates random start grid when it's selected from start menu
        /// </summary>
        public void CreateGrids()
        {
            if (NumberOfGames > 1)
            {
                SelectedGames = Menu.DisplayGameSelection(NumberOfGames);
            }
            else
            {
                SelectedGames[0] = 1;
            }

            Random randomInt = new Random();

            for (int game = 0; game < NumberOfGames; game++)
            {
                for (int line = 0; line < Game.Height; line++)
                {
                    for (int character = 0; character < Game.Width; character++)
                    {
                        if (randomInt.Next(5) == 1)
                        {
                            Game.GameGrid[line, character, game] = AliveCell;
                            Game.AliveCellCount[game]++;
                        }
                        else
                        {
                            Game.GameGrid[line, character, game] = DeadCell;
                        }
                    }
                }
            }

            Array.Copy(Game.AliveCellCount, LastAliveCellCount, Game.AliveCellCount.Length);
            Array.Copy(Game.GameGrid, NextGameGrid, Game.GameGrid.Length);

            SetUpdateTimer();
        }

        /// <summary>
        /// Creates custom start grid when selected from menu
        /// </summary>
        public void CreateCustomGrid()
        {
            for (int line = 0; line < Game.Height; line++)
            {
                for (int character = 0; character < Game.Width; character++)
                {
                    Game.GameGrid[line, character, 0] = DeadCell;
                }
            }

            Game.GameGrid[10, 10, 0] = AliveCell;
            Game.GameGrid[10, 11, 0] = AliveCell;
            Game.GameGrid[10, 12, 0] = AliveCell;
            Game.GameGrid[11, 10, 0] = AliveCell;
            Game.GameGrid[12, 10, 0] = AliveCell;
            Game.GameGrid[11, 12, 0] = AliveCell;
            Game.GameGrid[12, 12, 0] = AliveCell;
            Game.GameGrid[13, 11, 0] = AliveCell;
            Game.GameGrid[14, 11, 0] = AliveCell;
            Game.GameGrid[15, 11, 0] = AliveCell;
            Game.GameGrid[16, 11, 0] = AliveCell;
            Game.GameGrid[17, 10, 0] = AliveCell;
            Game.GameGrid[17, 12, 0] = AliveCell;
            Game.GameGrid[18, 12, 0] = AliveCell;
            Game.GameGrid[18, 10, 0] = AliveCell;
            Game.GameGrid[14, 10, 0] = AliveCell;
            Game.GameGrid[14, 12, 0] = AliveCell;
            Game.GameGrid[15,  9, 0] = AliveCell;
            Game.GameGrid[14,  8, 0] = AliveCell;
            Game.GameGrid[15, 13, 0] = AliveCell;
            Game.GameGrid[16, 14, 0] = AliveCell;

            Game.AliveCellCount[0] += 21;

            Array.Copy(Game.AliveCellCount, LastAliveCellCount, Game.AliveCellCount.Length);
            Array.Copy(Game.GameGrid, NextGameGrid, Game.GameGrid.Length);

            SetUpdateTimer();
        }

        /// <summary>
        /// Creates the Grid and sets up all the needed variables up, when reading from save file
        /// </summary>
        public void CreateGridFromFile()
        {
            if (NumberOfGames > 1)
            {
                SelectedGames = Menu.DisplayGameSelection(NumberOfGames);
            }
            else
            {
                SelectedGames[0] = 1;
            }

            SetUpdateTimer();
        }

        /// <summary>
        /// Sets up the timer that calls UpdateGrid method every second
        /// </summary>
        public void SetUpdateTimer()
        {
            UpdateTimer = new Timer(1000); 
            UpdateTimer.Elapsed += UpdateGrid;
            UpdateTimer.AutoReset = true;
            UpdateTimer.Enabled = true;

            //Infinite loop so the program doesn't stop
            while (true){}
        }

        /// <summary>
        /// Updates the grid, calls methods that check if cells are alive or dead
        /// </summary>
        public void UpdateGrid(Object source, ElapsedEventArgs e)
        {
            Pause.CheckForPauseOrSave(Game, UpdateTimer);

            Game.Iteration++;
            Game.AliveGridCount = NumberOfGames;

            GetAliveGrids();
            Array.Copy(Game.AliveCellCount, LastAliveCellCount, Game.AliveCellCount.Length);
            Array.Copy(NextGameGrid, Game.GameGrid, Game.GameGrid.Length);

            if (NumberOfGames > 1)
            {
                Menu.DrawEightGrids(Game, SelectedGames);
            }
            else
            {
                Menu.DrawGrid(Game);
            }

            for (int game = 0; game < NumberOfGames; game++)
            {
                for (int line = 0; line < Game.Height; line++)
                {
                    for (int character = 0; character < Game.Width; character++)
                    {
                        if (Game.GameGrid[line, character, game] == AliveCell)
                        {
                            CheckAndChangesIfCellAliveOrDead(line, character, true, game);
                        }
                        else
                        {
                            CheckAndChangesIfCellAliveOrDead(line, character, false, game);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a cell is dead or alive, changes changes it in grid accordingly
        /// </summary>
        public void CheckAndChangesIfCellAliveOrDead(int height, int width, bool alive, int gameNumber)
        {
            int aliveNeighbors = GetAliveNeighbors(height, width, gameNumber);

            if (alive)
            {
                if (aliveNeighbors < 2 || aliveNeighbors > 3)
                {
                    NextGameGrid[height, width, gameNumber] = DeadCell;
                    Game.AliveCellCount[gameNumber]--;
                }
                else
                {
                    NextGameGrid[height, width, gameNumber] = AliveCell;
                }
            }
            else
            {
                if (aliveNeighbors == 3)
                {
                    NextGameGrid[height, width, gameNumber] = AliveCell;
                    Game.AliveCellCount[gameNumber]++;
                }
                else
                {
                    NextGameGrid[height, width, gameNumber] = DeadCell;
                }
            }
        }

        /// <summary>
        /// Returns how many neighbor cells does a cell have
        /// </summary>
        public int GetAliveNeighbors(int height, int width, int gameNumber)
        {
            int aliveNeighbors = 0;
            int[,] neighbors = new int[,] { { -1, -1 }, { 0, -1 }, { 1, -1 }, 
                                            { -1,  0 },            { 1,  0 },
                                            { -1,  1 }, { 0,  1 }, { 1,  1 }, };

            for (int place = 0; place < 8; place++)
            {
                if (width + neighbors[place, 1] > -1 && width + neighbors[place, 1] < Game.Width)
                {
                    if (height + neighbors[place, 0] > -1 && height + neighbors[place, 0] < Game.Height)
                    {
                        if (Game.GameGrid[height + neighbors[place, 0], width + neighbors[place, 1], gameNumber] == AliveCell)
                        {
                            aliveNeighbors++;
                        }
                    }
                }
            }

            return aliveNeighbors;
        }

        /// <summary>
        /// Gets how many Alive grids there are 
        /// </summary>
        private void GetAliveGrids()
        {
            bool[] gridIsDead = new bool[NumberOfGames];

            for (int game = 0; game < NumberOfGames; game++)
            {
                gridIsDead[game] = true;
            }

            for (int game = 0; game < NumberOfGames; game++)
            {
                for (int line = 0; line < Game.Height; line++)
                {
                    for (int character = 0; character < Game.Width; character++)
                    {
                        if (Game.GameGrid[line, character, game] != NextGameGrid[line, character, game])
                        {
                            gridIsDead[game] = false;
                        }
                    }
                }
            }

            for (int game = 0; game < NumberOfGames; game++)
            {
                if (gridIsDead[game])
                {
                    Game.AliveGridCount--;
                }
            }

            if (Game.Iteration == 1)
            {
                Game.AliveGridCount = NumberOfGames;
            }
            else if(FreshGame == true)
            {
                Game.AliveGridCount = FreshAliveGridCount;
                FreshGame = false;
            }
        }
    }
}