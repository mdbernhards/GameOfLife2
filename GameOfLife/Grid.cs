using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameOfLife
{
    /// <summary>
    /// Handles Game of Life logic
    /// </summary>
    public class Grid
    {
        private int Height { get; set; }
        private int Width { get; set; }
        private bool[, ,] GameGrid { get; set; }
        public bool[, ,] NextGameGrid { get; set; }
        private int Iteration { get; set; }
        private int[] AliveCellCount { get; set; }
        private int[] LastAliveCellCount { get; set; }
        private int NumberOfGames { get; set; }
        private int AliveGridCount { get; set; }
        private int[] SelectedGames { get; set; }

        public const bool AliveCell = true;
        public const bool DeadCell = false;

        GameManager uiElements = new GameManager();

        /// <summary>
        /// Creates random start grid when it's selected from start menu
        /// </summary>
        public void CreateGrids(int height, int width, int numberOfGames)
        {
            SelectedGames = new int[8];
            NumberOfGames = numberOfGames;
            Height = height;
            Width = width;
            GameGrid = new bool[Height, Width, NumberOfGames];
            NextGameGrid = new bool[Height, Width, NumberOfGames];
            AliveCellCount = new int[NumberOfGames];
            LastAliveCellCount = new int[NumberOfGames];

            if (NumberOfGames > 1)
            {
                SelectedGames = uiElements.GameSelection(NumberOfGames);
            }
            else
            {
                SelectedGames[0] = 1;
            }

            Random randomInt = new Random();

            for (int game = 0; game < NumberOfGames; game++)
            {
                for (int line = 0; line < Height; line++)
                {
                    for (int character = 0; character < Width; character++)
                    {
                        if (randomInt.Next(5) == 1)
                        {
                            GameGrid[line, character, game] = AliveCell;
                            AliveCellCount[game]++;
                        }
                        else
                        {
                            GameGrid[line, character, game] = DeadCell;
                        }
                    }
                }
            }

            Array.Copy(AliveCellCount, LastAliveCellCount, AliveCellCount.Length);
            Array.Copy(GameGrid, NextGameGrid, GameGrid.Length);

            var task = UpdateGrid();
            task.Wait();
        }

        /// <summary>
        /// Creates custom start grid when selected from menu
        /// </summary>
        public void CreateCustomGrid(int height, int width)
        {
            NumberOfGames = 1;
            Height = height;
            Width = width;

            GameGrid = new bool[Height, Width, NumberOfGames];
            NextGameGrid = new bool[Height, Width, NumberOfGames];
            AliveCellCount = new int[NumberOfGames];
            LastAliveCellCount = new int[NumberOfGames];

            for (int line = 0; line < Height; line++)
            {
                for (int character = 0; character < Width; character++)
                {
                GameGrid[line, character, 0] = DeadCell;
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

            AliveCellCount[0] += 21;

            Array.Copy(AliveCellCount, LastAliveCellCount, AliveCellCount.Length);
            Array.Copy(GameGrid, NextGameGrid, GameGrid.Length);

            var task = UpdateGrid();
            task.Wait();
        }

        /// <summary>
        /// Creates the Grid and sets up all the needed variables up, when reading from save file
        /// </summary>
        public void CreateGridFromFile(bool[, ,] gameGrid, int iteration, int[] aliveCellCount)
        {
            NumberOfGames = gameGrid.GetLength(2);
            LastAliveCellCount = new int[NumberOfGames];
            SelectedGames = new int[8];

            GameGrid = new bool[gameGrid.GetLength(0), gameGrid.GetLength(1), NumberOfGames];
            NextGameGrid = new bool[gameGrid.GetLength(0), gameGrid.GetLength(1), NumberOfGames];
            Height = gameGrid.GetLength(0);
            Width = gameGrid.GetLength(1);

            Iteration = iteration;
            AliveCellCount = aliveCellCount;

            Array.Copy(AliveCellCount, LastAliveCellCount, AliveCellCount.Length);
            Array.Copy(gameGrid, GameGrid, gameGrid.Length);
            Array.Copy(gameGrid, NextGameGrid, gameGrid.Length);

            if (NumberOfGames > 1)
            {
                SelectedGames = uiElements.GameSelection(NumberOfGames);
            }
            else
            {
                SelectedGames[0] = 1;
            }

            var task = UpdateGrid();
            task.Wait();
        }

        /// <summary>
        /// Updates the grid every second, calls methods that check if cells are alive or dead
        /// </summary>
        public async Task UpdateGrid()
        {
            do
            {
                uiElements.CheckForPauseOrSave(GameGrid, Iteration, LastAliveCellCount, NumberOfGames);
                Iteration++;

                if(Iteration == 1)
                {
                    AliveGridCount = NumberOfGames * 2;
                }
                else
                {
                    AliveGridCount = NumberOfGames;
                }

                GetAliveGrids();
                Array.Copy(AliveCellCount, LastAliveCellCount, AliveCellCount.Length);
                Array.Copy(NextGameGrid, GameGrid, GameGrid.Length);

                if(NumberOfGames > 1)
                {
                    uiElements.DrawEightGrids(GameGrid, Iteration, AliveCellCount, Height, Width, SelectedGames, AliveGridCount);
                }
                else
                {
                    uiElements.DrawGrid(GameGrid, Iteration, AliveCellCount.Sum(), Height, Width, AliveGridCount);
                }

                for (int game = 0; game < NumberOfGames; game++)
                {
                    for (int line = 0; line < Height; line++)
                    {
                        for (int character = 0; character < Width; character++)
                        {
                            if (GameGrid[line, character, game] == AliveCell)
                            {
                                CheckIfCellAlive(line, character, true, game);
                            }
                            else
                            {
                                CheckIfCellAlive(line, character, false, game);
                            }
                        }
                    }
                }

                await Task.Delay(1000);

            } while (true);
        }

        /// <summary>
        /// Checks if a cell is dead or alive
        /// </summary>
        private void CheckIfCellAlive(int height, int width, bool alive, int gameNumber)
        {
            int aliveNeighbors = GetAliveNeighbors(height, width, gameNumber);

            if (alive)
            {
                if (aliveNeighbors < 2 || aliveNeighbors > 3)
                {
                    NextGameGrid[height, width, gameNumber] = DeadCell;
                    AliveCellCount[gameNumber]--;
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
                    AliveCellCount[gameNumber]++;
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
        private int GetAliveNeighbors(int height, int width, int gameNumber)
        {
            int aliveNeighbors = 0;

            int[,] neighbors = new int[,] { { -1, -1 }, { 0, -1 }, { 1, -1 }, 
                                            { -1,  0 },            { 1,  0 },
                                            { -1,  1 }, { 0,  1 }, { 1,  1 }, };

            for (int place = 0; place < 8; place++)
            {
                if (width + neighbors[place, 1] > -1 && width + neighbors[place, 1] < Width)
                {
                    if (height + neighbors[place, 0] > -1 && height + neighbors[place, 0] < Height)
                    {
                        if (GameGrid[height + neighbors[place, 0], width + neighbors[place, 1], gameNumber] == AliveCell)
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
            for (int game = 0; game < NumberOfGames; game++)
            {
                if(AliveCellCount[game] == LastAliveCellCount[game])
                {
                    AliveGridCount--;
                }
            }
        }
    }
}