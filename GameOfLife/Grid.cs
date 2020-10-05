using System;
using System.Threading;

namespace GameOfLife
{
    public class Grid
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string[,] GameGrid { get; set; }
        public string[,] NextGameGrid { get; set; }
        public int Iteration { get; set; }
        public int AliveCellCount { get; set; }
        private int LastAliveCellCount { get; set; }

        public void CreateGrid(int height, int width)
        {
            Height = height;
            Width = width;
            GameGrid = new string[Height, Width];
            NextGameGrid = new string[Height, Width];

            Random randomInt = new Random();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (randomInt.Next(5) == 1)
                    {
                        GameGrid[i, j] = "█";
                        AliveCellCount++;
                    }
                    else
                    {
                        GameGrid[i, j] = " ";
                    }
                }
            }

            LastAliveCellCount = AliveCellCount;
            Array.Copy(GameGrid, NextGameGrid, GameGrid.Length);

            updateGrid();
        }

        public void CustomGrid(int x, int y)
        {
            Height = x;
            Width = y;

            GameGrid = new string[Height, Width];
            NextGameGrid = new string[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    GameGrid[i, j] = " ";
                }
            }

            GameGrid[10, 10] = "█";
            GameGrid[10, 11] = "█";
            GameGrid[10, 12] = "█";
            GameGrid[11, 10] = "█";
            GameGrid[12, 10] = "█";
            GameGrid[11, 12] = "█";
            GameGrid[12, 12] = "█";
            GameGrid[13, 11] = "█";
            GameGrid[14, 11] = "█";
            GameGrid[15, 11] = "█";
            GameGrid[16, 11] = "█";
            GameGrid[17, 10] = "█";
            GameGrid[17, 12] = "█";
            GameGrid[18, 12] = "█";
            GameGrid[18, 10] = "█";
            GameGrid[14, 10] = "█";
            GameGrid[14, 12] = "█";
            GameGrid[15,  9] = "█";
            GameGrid[14,  8] = "█";
            GameGrid[15, 13] = "█";
            GameGrid[16, 14] = "█";

            AliveCellCount += 21;
            LastAliveCellCount = AliveCellCount;

            Array.Copy(GameGrid, NextGameGrid, GameGrid.Length);

            updateGrid();
        }

        public void updateGrid()
        {
            do
            {
                CheckForPauseOrSave();

                Iteration++;
                LastAliveCellCount = AliveCellCount;
                Array.Copy(NextGameGrid, GameGrid, GameGrid.Length);
                ShowGrid();

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (GameGrid[i, j] == "█")
                        {
                            CheckIfAlive(i, j, true);
                        }
                        else
                        {
                            CheckIfAlive(i, j, false);
                        }
                    }
                }

                Thread.Sleep(1000);

            } while (true);
        }

        void CheckForPauseOrSave()
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
                        GameSave gameSave = new GameSave();
                        gameSave.SaveGame(GameGrid, Iteration, LastAliveCellCount);
                        Console.WriteLine("Game Saved!");
                    }
                } while (true);
            }
        }

        void ShowGrid()
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
                    Console.Write(GameGrid[i, j]);
                }

                Console.WriteLine();
            }
        }

        void CheckIfAlive(int x, int y, bool alive)
        {
            int aliveNeighbors = GetAliveNeighbors(x,y);

            if (alive)
            {
                if (aliveNeighbors < 2 || aliveNeighbors > 3)
                {
                    NextGameGrid[x, y] = " ";
                    AliveCellCount--;
                }
                else
                {
                    NextGameGrid[x, y] = "█";
                }
            }
            else
            {
                if (aliveNeighbors == 3)
                {
                    NextGameGrid[x, y] = "█";
                    AliveCellCount++;
                }
                else
                {
                    NextGameGrid[x, y] = " ";
                }
            }
        }

        int GetAliveNeighbors(int x, int y)
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
                        if (GameGrid[x + neighbors[i, 0], y + neighbors[i, 1]] == "█")
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