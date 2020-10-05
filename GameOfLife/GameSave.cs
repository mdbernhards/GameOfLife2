using System;
using System.IO;

namespace GameOfLife
{
    //Saves and loads saved Game of Life state
    public class GameSave
    {
        public const string filePath = "GameSave.txt";

        //Saves game state when it's paused and S is pressed
        public void SaveGame(bool[,] gameGrid, int iteration, int aliveCellCount)
        {
            using var swGrid = new StreamWriter(filePath);

            swGrid.Write(iteration);
            swGrid.Write("\n");
            swGrid.Write(aliveCellCount);
            swGrid.Write("\n");

            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                for (int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    if (gameGrid[i, j] == true)
                    {
                        swGrid.Write("█");
                    }
                    else
                    {
                        swGrid.Write(" ");
                    }
                }
                swGrid.Write("\n");
            }

            swGrid.Flush();
            swGrid.Close();
        }

        //Loads saved game state when needed
        public void LoadSave()
        {
            string gridInput = File.ReadAllText(filePath);

            
            bool[,] gameGrid = new bool[gridInput.Split('\n').Length , gridInput.Split('\n')[2].Length];

            var gridRows = gridInput.Split('\n');
            

            for (int i = 2; i < gridRows.Length ; i++)
            {
                char[] gridCol = gridRows[i].ToCharArray();

                for (int j = 0; j < gridCol.Length; j++)
                {
                    if (gridCol[j].ToString() == "█")
                    {
                        gameGrid[i, j] = true;
                    }
                    else
                    {
                        gameGrid[i, j] = false;
                    }
                }
            }

            //move to Grid
            Grid grid = new Grid
            {
                GameGrid = new bool[gameGrid.GetLength(0), gameGrid.GetLength(1)],
                NextGameGrid = new bool[gameGrid.GetLength(0), gameGrid.GetLength(1)],
                Height = gameGrid.GetLength(0),
                Width = gameGrid.GetLength(1),

                Iteration = int.Parse(gridRows[0]) - 1,
                AliveCellCount = int.Parse(gridRows[1])
            };

            Array.Copy(gameGrid, grid.GameGrid, gameGrid.Length);
            Array.Copy(gameGrid, grid.NextGameGrid, gameGrid.Length);

            var task = grid.UpdateGrid();
            task.Wait();
        }
    }
}