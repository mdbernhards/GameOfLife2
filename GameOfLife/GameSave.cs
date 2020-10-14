using Newtonsoft.Json;
using System.IO;
using System.IO.Abstractions;

namespace GameOfLife
{
    /// <summary>
    /// Class that saves and loads Game of Life
    /// </summary>
    public class GameSave : IGameSave
    {
        private readonly IFileSystem fileSystem;

        public const string filePath = "Saves/GameSave.json";
        public const string folderPath = "Saves/";

        public GameSave(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public GameSave() : this( fileSystem: new FileSystem())
        {
        }

        /// <summary>
        /// Saves game state when called
        /// </summary>
        public void SaveGame(bool[, ,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount)
        {
            FileInfo file = new FileInfo(folderPath);
            file.Directory.Create();


            string[] filePaths = Directory.GetFiles(folderPath);

            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }

            Game game = new Game(gameGrid, iteration - 1, aliveCellCount, aliveGridCount);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(game));
        }

        /// <summary>
        /// Loads and returns saved game state when called
        /// </summary>
        public Game ReadSaveFile()
        {
            //var fileInfo = fileSystem.Directory.GetFiles(folderPath);
            string fileText = fileSystem.File.ReadAllText(filePath);

            Game game = JsonConvert.DeserializeObject<Game>(fileText);
            return game;
        }
    }
}