using System.IO;

namespace GameOfLife
{
    /// <summary>
    /// Saves and loads saved Game of Life state
    /// </summary>
    public class GameSave
    {
        public const string filePath = "GameSave.txt";

        /// <summary>
        /// Saves game state when it's paused and S is pressed
        /// </summary>
        public void SaveGame(bool[, ,] gameGrid, int iteration, int aliveCellCount)
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
                    if (gameGrid[i, j, 0] == true)
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

        /// <summary>
        /// Loads saved game state when needed
        /// </summary>
        public (bool[,,], int, int) ReadSaveFile()
        {
            string gridInput = File.ReadAllText(filePath);
            bool[, ,] gameGrid = new bool[gridInput.Split('\n').Length , gridInput.Split('\n')[2].Length, 1];

            var gridRows = gridInput.Split('\n');

            for (int i = 2; i < gridRows.Length ; i++)
            {
                char[] gridCol = gridRows[i].ToCharArray();

                for (int j = 0; j < gridCol.Length; j++)
                {
                    if (gridCol[j].ToString() == "█")
                    {
                        gameGrid[i, j, 0] = true;
                    }
                    else
                    {
                        gameGrid[i, j, 0] = false;
                    }
                }
            }

            int iteration = int.Parse(gridRows[0]) - 1;
            int aliveCellCount = int.Parse(gridRows[1]);

            return (gameGrid, iteration, aliveCellCount);
        }
    }
}