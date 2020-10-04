using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameOfLife
{
    public class GameSave
    {

        public void SaveGame(string[,] gameGrid)
        {

            using (var sw = new StreamWriter("GameSave.txt"))
            {
                for (int i = 0; i < gameGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < gameGrid.GetLength(1); j++)
                    {
                        sw.Write(gameGrid[i, j]);
                    }
                    sw.Write("\n");
                }

                sw.Flush();
                sw.Close();
            }
        }

        public void LoadSave()
        {
            String input = File.ReadAllText("GameSave.txt");

            int i = 0;

            string[,] gameGrid = new string[input.Split('\n').Length - 1, input.Split('\n')[0].Length];

            foreach (var row in input.Split('\n'))
            {
                char[] col = row.ToCharArray();

                for (int j = 0; j < row.Length; j++)
                {
                    gameGrid[i, j] = col[j].ToString();
                }
                i++;
            }

            Grid grid = new Grid
            {

                GameGrid = new string[gameGrid.GetLength(0), gameGrid.GetLength(1)],
                NextGameGrid = new string[gameGrid.GetLength(0), gameGrid.GetLength(1)],
                Height = gameGrid.GetLength(0),
                Width = gameGrid.GetLength(1)
            };

            Array.Copy(gameGrid, grid.GameGrid, gameGrid.Length);
            Array.Copy(gameGrid, grid.NextGameGrid, gameGrid.Length);

            grid.updateGrid();

        }
    }
}
