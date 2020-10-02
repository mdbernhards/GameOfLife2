using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public class Grid
    {
        private int _x { get; set; }
        private int _y { get; set; }
        public string[,] GameGrid { get; set; }

        public void CreateGrid(int x, int y)
        {
            _x = x;
            _y = y;
            Random randomInt = new Random();

            GameGrid = new string[_x, _y];

            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    if (randomInt.Next(10) == 1)
                    {
                        GameGrid[i, j] = "0";
                    }
                    else
                    {
                        GameGrid[i, j] = " ";
                    }
                }
            }

            updateGrid();
        }

        public void CreateCustomGrid(int x, int y)
        {
            _x = x;
            _y = y;

            GameGrid = new string[_x, _y];

            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                        GameGrid[i, j] = " ";
                }
            }

            GameGrid[10, 10] = "0";
            GameGrid[10, 11] = "0";
            GameGrid[10, 12] = "0";
            GameGrid[11, 10] = "0";
            GameGrid[12, 10] = "0";
            GameGrid[11, 12] = "0";
            GameGrid[12, 12] = "0";
            GameGrid[13, 11] = "0";
            GameGrid[14, 11] = "0";
            GameGrid[15, 11] = "0";
            GameGrid[16, 11] = "0";
            GameGrid[17, 10] = "0";
            GameGrid[17, 11] = "0";
            GameGrid[18, 11] = "0";
            GameGrid[18, 10] = "0";
            GameGrid[14, 10] = "0";
            GameGrid[14, 12] = "0";
            GameGrid[15, 9] = "0";
            GameGrid[14, 8] = "0";
            GameGrid[15, 13] = "0";
            GameGrid[16, 14] = "0";


            updateGrid();

        }

        void updateGrid()
        {
            do
            {
                showGrid();

                for (int i = 0; i < _x; i++)
                {
                    for (int j = 0; j < _y; j++)
                    {
                        if(GameGrid[i,j] == "0")
                        {
                            CheckIfAlive(i, j, true);
                        }
                        else
                        {
                            CheckIfAlive(i, j, false);
                        }
                    }
                }


                Thread.Sleep(100);

            } while (true);
        }

        void showGrid()
        {
            Console.Clear();

            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
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
                if(aliveNeighbors < 2 || aliveNeighbors > 3)
                {
                    GameGrid[x, y] = " ";
                }
            }
            else
            {
                if(aliveNeighbors == 3)
                {
                    GameGrid[x, y] = "0";
                }
            }
        }

        int GetAliveNeighbors(int x, int y)
        {
            int aliveNeighbors = 0;

            int[,] neighbors = new int[,] { { 0, 1 }, { 1, 0 }, { 1, 1 }, { 0, -1 }, { -1, 0 }, { -1, -1 }, { 1, -1 }, { -1, 1 }, };

            for (int i = 0; i < 8; i++)
            {
                if (x + neighbors[i, 0] > -1 && x + neighbors[i, 0] < _x && y + neighbors[i, 1] > -1 && y + neighbors[i, 1] < _y)
                {
                    if (GameGrid[x + neighbors[i, 0], y + neighbors[i, 1]] == "0")
                    {
                        aliveNeighbors++;
                    }
                }
            }

            return aliveNeighbors;
        }
    }
}
