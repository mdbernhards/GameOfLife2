using System;
using System.Threading.Tasks;

namespace GameOfLife
{
    //Handles Game of Life logic
    public class Grid
    {
        private int Height { get; set; }
        private int Width { get; set; }
        public bool[, ,] GameGrid { get; set; }
        public bool[, ,] NextGameGrid { get; set; }
        public int Iteration { get; set; }
        public int AliveCellCount { get; set; }
        private int LastAliveCellCount { get; set; }
        public int NumberOfGames { get; set; }

        public const bool AliveCell = true;
        public const bool DeadCell = false;

        UIElements uiElements = new UIElements();

        //Creates random start grid when it's selected from start menu
        public void CreateGrids(int height, int width, int numberOfGames)
        {
            NumberOfGames = numberOfGames;
            Height = height;
            Width = width;
            GameGrid = new bool[Height, Width, NumberOfGames];
            NextGameGrid = new bool[Height, Width, NumberOfGames];

            Random randomInt = new Random();

            for (int k = 0; k < NumberOfGames; k++)
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (randomInt.Next(5) == 1)
                        {
                            GameGrid[i, j, k] = AliveCell;
                            AliveCellCount++;
                        }
                        else
                        {
                            GameGrid[i, j, k] = DeadCell;
                        }
                    }
                }
            }
            LastAliveCellCount = AliveCellCount;
            Array.Copy(GameGrid, NextGameGrid, GameGrid.Length);

            var task = UpdateGrid();
            task.Wait();
        }

        //Creates custom start grid when selected from menu
        public void CreateCustomGrid(int height, int width)
        {
            NumberOfGames = 1;
            Height = height;
            Width = width;

            GameGrid = new bool[Height, Width, NumberOfGames];
            NextGameGrid = new bool[Height, Width, NumberOfGames];
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        GameGrid[i, j, 0] = DeadCell;
                    }
                }

            GameGrid[10, 10, 0] = AliveCell;
            GameGrid[10, 11, 0] = AliveCell;
            GameGrid[10, 12, 0] = AliveCell;
            GameGrid[11, 10, 0] = AliveCell;
            GameGrid[12, 10, 0] = AliveCell;
            GameGrid[11, 12, 0] = AliveCell;
            GameGrid[12, 12, 0] = AliveCell;
            GameGrid[13, 11, 0] = AliveCell;
            GameGrid[14, 11, 0] = AliveCell;
            GameGrid[15, 11, 0] = AliveCell;
            GameGrid[16, 11, 0] = AliveCell;
            GameGrid[17, 10, 0] = AliveCell;
            GameGrid[17, 12, 0] = AliveCell;
            GameGrid[18, 12, 0] = AliveCell;
            GameGrid[18, 10, 0] = AliveCell;
            GameGrid[14, 10, 0] = AliveCell;
            GameGrid[14, 12, 0] = AliveCell;
            GameGrid[15,  9, 0] = AliveCell;
            GameGrid[14,  8, 0] = AliveCell;
            GameGrid[15, 13, 0] = AliveCell;
            GameGrid[16, 14, 0] = AliveCell;

            AliveCellCount += 21;
            LastAliveCellCount = AliveCellCount;

            Array.Copy(GameGrid, NextGameGrid, GameGrid.Length);

            var task = UpdateGrid();
            task.Wait();
        }

        //Creates the Grid and sets up all the needed variables up, when reading from save file
        public void CreateGridFromFile(bool[, ,] gameGrid, int iteration, int aliveCellCount)
        {
            NumberOfGames = 1;

            GameGrid = new bool[gameGrid.GetLength(0), gameGrid.GetLength(1), NumberOfGames];
            NextGameGrid = new bool[gameGrid.GetLength(0), gameGrid.GetLength(1), NumberOfGames];
            Height = gameGrid.GetLength(0);
            Width = gameGrid.GetLength(1);

            Iteration = iteration;
            AliveCellCount = aliveCellCount;
            

            Array.Copy(gameGrid, GameGrid, gameGrid.Length);
            Array.Copy(gameGrid, NextGameGrid, gameGrid.Length);

            var task = UpdateGrid();
            task.Wait();
        }

        //Updates the grid every second, calls methods that check if cells are alive or dead
        public async Task UpdateGrid()
        {
            do
            {
                uiElements.CheckForPauseOrSave(GameGrid, Iteration, LastAliveCellCount);
                
                LastAliveCellCount = AliveCellCount;
                Iteration++;
                Array.Copy(NextGameGrid, GameGrid, GameGrid.Length);

                uiElements.DrawGrid(GameGrid, Iteration, AliveCellCount, Height, Width);

                for (int k = 0; k < NumberOfGames; k++)
                {
                    for (int i = 0; i < Height; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            if (GameGrid[i, j, k] == AliveCell)
                            {
                                CheckIfCellAlive(i, j, true, k);
                            }
                            else
                            {
                                CheckIfCellAlive(i, j, false, k);
                            }
                        }
                    }
                }

                await Task.Delay(1000);

            } while (true);
        }

        //Checks if a cell is dead or alive
        private void CheckIfCellAlive(int height, int width, bool alive, int gameNumber)
        {
            int aliveNeighbors = GetAliveNeighbors(height, width, gameNumber);

            if (alive)
            {
                if (aliveNeighbors < 2 || aliveNeighbors > 3)
                {
                    NextGameGrid[height, width, gameNumber] = DeadCell;
                    AliveCellCount--;
                }
                else
                {
                    NextGameGrid[height, width, gameNumber] = AliveCell; //make constant, true false
                }
            }
            else
            {
                if (aliveNeighbors == 3)
                {
                    NextGameGrid[height, width, gameNumber] = AliveCell;
                    AliveCellCount++;
                }
                else
                {
                    NextGameGrid[height, width, gameNumber] = DeadCell;
                }
            }
        }

        //Returns how many neighbor cells does a cell have
        private int GetAliveNeighbors(int height, int width, int gameNumber)
        {
            int aliveNeighbors = 0;

            int[,] neighbors = new int[,] { { -1, -1 }, { 0, -1 }, { 1, -1 }, 
                                            { -1,  0 },            { 1,  0 },
                                            { -1,  1 }, { 0,  1 }, { 1,  1 }, };

            for (int i = 0; i < 8; i++)
            {
                if (width + neighbors[i, 1] > -1 && width + neighbors[i, 1] < Width)
                {
                    if (height + neighbors[i, 0] > -1 && height + neighbors[i, 0] < Height)
                    {
                        if (GameGrid[height + neighbors[i, 0], width + neighbors[i, 1], gameNumber] == AliveCell)
                        {
                            aliveNeighbors++;
                        }
                    }
                }
            }

            return aliveNeighbors;
        }
    }
}