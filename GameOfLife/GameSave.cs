using System.IO;

namespace GameOfLife
{
    /// <summary>
    /// Saves and loads saved Game of Life state
    /// </summary>
    public class GameSave
    {
        public const string filePath = "Saves/GameSave.json";
        public const string folderPath = "Saves/";

        /// <summary>
        /// Saves game state when it's paused and S is pressed
        /// </summary>
        public static void SaveGame(bool[, ,] gameGrid, int iteration, int[] aliveCellCount, int numberOfGames)
        {
            FileInfo file = new FileInfo(folderPath);
            file.Directory.Create();

            string[] filePaths = Directory.GetFiles(folderPath);

            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }

            for (int game = 0; game < numberOfGames; game++)
            {
                using var swGrid = new StreamWriter(filePath.Insert(14, (game+1).ToString()));

                swGrid.Write(iteration);
                swGrid.Write("\n");

                swGrid.Write(aliveCellCount[game]);
                swGrid.Write("\n");

                for (int line = 0; line < gameGrid.GetLength(0); line++)
                {
                    for (int character = 0; character < gameGrid.GetLength(1); character++)
                    {
                        if (gameGrid[line, character, game] == true)
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
        public Game ReadSaveFile()
        {
            bool[,,] gameGrid;
            string[] gridRows;
            int iteration = 0;

            DirectoryInfo dir = new DirectoryInfo("Saves/");
            int fileCount = dir.GetFiles().Length;
            int[] aliveCellCount = new int[fileCount];

            string gridInput = File.ReadAllText(filePath.Insert(14, 1.ToString()));
            gameGrid = new bool[gridInput.Split('\n').Length, gridInput.Split('\n')[2].Length, fileCount];

            for (int file = 0; file < fileCount; file++)
            {
                gridInput = File.ReadAllText(filePath.Insert(14, (file + 1).ToString()));
                gridRows = gridInput.Split('\n');

                for (int line = 2; line < gridRows.Length; line++)
                {
                    char[] gridCol = gridRows[line].ToCharArray();

                    for (int character = 0; character < gridCol.Length; character++)
                    {
                        if (gridCol[character].ToString() == "█")
                        {
                            gameGrid[line, character, file] = true;
                        }
                        else
                        {
                            gameGrid[line, character, file] = false;
                        }
                    }
                }

                iteration = int.Parse(gridRows[0]) - 1;
                aliveCellCount[file] = int.Parse(gridRows[1]);
            }

            Game game = new Game(gameGrid, iteration, aliveCellCount);

            return game;
        }
    }
}