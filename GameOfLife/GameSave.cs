using System.IO;

namespace GameOfLife
{
    /// <summary>
    /// Saves and loads saved Game of Life state
    /// </summary>
    public class GameSave
    {
        public const string filePath = "Saves/GameSave.txt";
        public const string folderPath = "Saves/";

        /// <summary>
        /// Saves game state when it's paused and S is pressed
        /// </summary>
        public void SaveGame(bool[, ,] gameGrid, int iteration, int[] aliveCellCount, int numberOfGames)
        {
            FileInfo file = new FileInfo(folderPath);
            file.Directory.Create();

            for (int k = 0; k < numberOfGames; k++)
            {
                using var swGrid = new StreamWriter(filePath.Insert(14, (k+1).ToString()));

                swGrid.Write(iteration);
                swGrid.Write("\n");

                swGrid.Write(aliveCellCount[k]);
                swGrid.Write("\n");

                for (int i = 0; i < gameGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < gameGrid.GetLength(1); j++)
                    {
                        if (gameGrid[i, j, k] == true)
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
            
        }

        /// <summary>
        /// Loads saved game state when needed
        /// </summary>
        public (bool[,,], int, int[]) ReadSaveFile()
        {
            bool[,,] gameGrid;
            string[] gridRows;
            int iteration = 0;

            DirectoryInfo dir = new DirectoryInfo("Saves/");
            int fileCount = dir.GetFiles().Length;
            int[] aliveCellCount = new int[fileCount];

            string gridInput = File.ReadAllText(filePath.Insert(14, 1.ToString()));
            gameGrid = new bool[gridInput.Split('\n').Length, gridInput.Split('\n')[2].Length, fileCount];

            for (int k = 0; k < fileCount; k++)
            {
                gridInput = File.ReadAllText(filePath.Insert(14, (k + 1).ToString()));
                gridRows = gridInput.Split('\n');

                for (int i = 2; i < gridRows.Length; i++)
                {
                    char[] gridCol = gridRows[i].ToCharArray();

                    for (int j = 0; j < gridCol.Length; j++)
                    {
                        if (gridCol[j].ToString() == "█")
                        {
                            gameGrid[i, j, k] = true;
                        }
                        else
                        {
                            gameGrid[i, j, k] = false;
                        }
                    }
                }

                iteration = int.Parse(gridRows[0]) - 1;
                aliveCellCount[k] = int.Parse(gridRows[1]);
            }
           
            return (gameGrid, iteration, aliveCellCount);
        }
    }
}