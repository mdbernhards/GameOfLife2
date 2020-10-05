using System;
using System.IO;

namespace GameOfLife
{
    public class GameSave
    {
        public void SaveGame(string[,] gameGrid, int iteration, int aliveCellCount)
        {

            using var swGrid = new StreamWriter("GridSave.txt");
            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                for (int j = 0; j < gameGrid.GetLength(1); j++)
                {
                    swGrid.Write(gameGrid[i, j]);
                }
                swGrid.Write("\n");
            }

            swGrid.Flush();
            swGrid.Close();

            using var swInfo = new StreamWriter("InfoSave.txt");

            swInfo.Write(iteration);
            swInfo.Write("\n");
            swInfo.Write(aliveCellCount);

            swInfo.Flush();
            swInfo.Close();
        }

        public void LoadSave()
        {
            string gridInput = File.ReadAllText("GridSave.txt");

            int i = 0;
            string[,] gameGrid = new string[gridInput.Split('\n').Length - 1, gridInput.Split('\n')[0].Length];

            foreach (var row in gridInput.Split('\n'))
            {
                char[] col = row.ToCharArray();

                for (int j = 0; j < row.Length; j++)
                {
                    gameGrid[i, j] = col[j].ToString();
                }

                i++;
            }

            string infoInput = File.ReadAllText("InfoSave.txt");
            var info = infoInput.Split('\n');

            Grid grid = new Grid
            {
                GameGrid = new string[gameGrid.GetLength(0), gameGrid.GetLength(1)],
                NextGameGrid = new string[gameGrid.GetLength(0), gameGrid.GetLength(1)],
                Height = gameGrid.GetLength(0),
                Width = gameGrid.GetLength(1),

                Iteration = int.Parse(info[0]) - 1,
                AliveCellCount = int.Parse(info[1])
            };

            Array.Copy(gameGrid, grid.GameGrid, gameGrid.Length);
            Array.Copy(gameGrid, grid.NextGameGrid, gameGrid.Length);

            grid.UpdateGrid();
        }
    }
}