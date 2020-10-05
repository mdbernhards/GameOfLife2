using System;
using System.Threading.Tasks;

namespace GameOfLife
{
    //Handles Game of Life logic
    public class Grid
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public bool[,] GameGrid { get; set; }
        public bool[,] NextGameGrid { get; set; }
        public int Iteration { get; set; }
        public int AliveCellCount { get; set; }
        private int LastAliveCellCount { get; set; }

        public const bool AliveCell = true;
        public const bool DeadCell = false;

        //Creates random start grid when it's selected from start menu
        public void CreateGrid(int height, int width)
        {
            Height = height;
            Width = width;
            GameGrid = new bool[Height, Width];
            NextGameGrid = new bool[Height, Width];

            Random randomInt = new Random();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (randomInt.Next(5) == 1)
                    {
                        GameGrid[i, j] = AliveCell;
                        AliveCellCount++;
                    }
                    else
                    {
                        GameGrid[i, j] = DeadCell;
                    }
                }
            }

            LastAliveCellCount = AliveCellCount;
            Array.Copy(GameGrid, NextGameGrid, GameGrid.Length);

            var task = UpdateGrid();
            task.Wait();
        }

        //Creates custom start grid when selected from menu
        public void CreateCustomGrid(int x, int y)
        {
            Height = x;
            Width = y;

            GameGrid = new bool[Height, Width];
            NextGameGrid = new bool[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    GameGrid[i, j] = DeadCell;
                }
            }

            GameGrid[10, 10] = AliveCell;
            GameGrid[10, 11] = AliveCell;
            GameGrid[10, 12] = AliveCell;
            GameGrid[11, 10] = AliveCell;
            GameGrid[12, 10] = AliveCell;
            GameGrid[11, 12] = AliveCell;
            GameGrid[12, 12] = AliveCell;
            GameGrid[13, 11] = AliveCell;
            GameGrid[14, 11] = AliveCell;
            GameGrid[15, 11] = AliveCell;
            GameGrid[16, 11] = AliveCell;
            GameGrid[17, 10] = AliveCell;
            GameGrid[17, 12] = AliveCell;
            GameGrid[18, 12] = AliveCell;
            GameGrid[18, 10] = AliveCell;
            GameGrid[14, 10] = AliveCell;
            GameGrid[14, 12] = AliveCell;
            GameGrid[15,  9] = AliveCell;
            GameGrid[14,  8] = AliveCell;
            GameGrid[15, 13] = AliveCell;
            GameGrid[16, 14] = AliveCell;

            AliveCellCount += 21;
            LastAliveCellCount = AliveCellCount;

            Array.Copy(GameGrid, NextGameGrid, GameGrid.Length);

            var task = UpdateGrid();
            task.Wait();
        }


        //Updates the grid every second, calls methods that check if cells are alive or dead
        public async Task UpdateGrid()
        {
            do
            {
                CheckForPauseOrSave();
                
                LastAliveCellCount = AliveCellCount;
                Iteration++;
                Array.Copy(NextGameGrid, GameGrid, GameGrid.Length);

                ShowGrid();

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (GameGrid[i, j] == AliveCell)
                        {
                            CheckIfCellAlive(i, j, true);
                        }
                        else
                        {
                            CheckIfCellAlive(i, j, false);
                        }
                    }
                }

                await Task.Delay(1000);

            } while (true);
        }

        //Checks if Game of Life needs to be: paused, unpaused or saved
        private void CheckForPauseOrSave() //move to UIElements
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
                        GameSave gameSave = new GameSave(); //constructor
                        gameSave.SaveGame(GameGrid, Iteration, LastAliveCellCount);
                        Console.WriteLine("Game Saved!");
                    }
                } while (true);
            }
        }

        //Draws the grid every time it has been updated
        private void ShowGrid() //move to UIElements
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("Iteration: " + Iteration);
            Console.WriteLine("Alive cell count: " + AliveCellCount);
            Console.WriteLine("");
            Console.WriteLine("Press Space to pause and unpause");
            Console.WriteLine("While Paused press S to save");
            Console.WriteLine("");

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if(GameGrid[i, j] == AliveCell)
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

        //Checks if a cell is dead or alive
        private void CheckIfCellAlive(int x, int y, bool alive)
        {
            int aliveNeighbors = GetAliveNeighbors(x,y);

            if (alive)
            {
                if (aliveNeighbors < 2 || aliveNeighbors > 3)
                {
                    NextGameGrid[x, y] = DeadCell;
                    AliveCellCount--;
                }
                else
                {
                    NextGameGrid[x, y] = AliveCell; //make constant, true false
                }
            }
            else
            {
                if (aliveNeighbors == 3)
                {
                    NextGameGrid[x, y] = AliveCell;
                    AliveCellCount++;
                }
                else
                {
                    NextGameGrid[x, y] = DeadCell;
                }
            }
        }

        //Returns how many neighbor cells does a cell have
        private int GetAliveNeighbors(int x, int y)
        {
            int aliveNeighbors = 0;

            int[,] neighbors = new int[,] { { -1, -1 }, { 0, -1 }, { 1, -1 }, 
                                            { -1,  0 },            { 1,  0 },
                                            { -1,  1 }, { 0,  1 }, { 1,  1 }, };

            for (int i = 0; i < 8; i++)
            {
                if (y + neighbors[i, 1] > -1 && y + neighbors[i, 1] < Width)
                {
                    if (x + neighbors[i, 0] > -1 && x + neighbors[i, 0] < Height)
                    {
                        if (GameGrid[x + neighbors[i, 0], y + neighbors[i, 1]] == AliveCell)
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