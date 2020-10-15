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
        private const string filePath = "Saves/GameSave.json";
        private const string folderPath = "Saves/";

        /// <summary>
        /// Saves and loads Game of Life
        /// </summary>
        public GameSave(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Saves and loads Game of Life
        /// </summary>
        public GameSave() : this( fileSystem: new FileSystem())
        {
        }

        /// <summary>
        /// Saves game state when called
        /// </summary>
        /// <param name="game">Game object that stores gameGrid, aliveCellCount, aliveGridCount and iteration</param>
        public void SaveGame(Games game)
        {
            FileInfo file = new FileInfo(folderPath);
            file.Directory.Create();

            string[] filePaths = Directory.GetFiles(folderPath);

            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, JsonConvert.SerializeObject(game));
        }

        /// <summary>
        /// Loads and returns saved game state when called
        /// </summary>
        public Games ReadSaveFile()
        {
            string fileText = fileSystem.File.ReadAllText(filePath);

            Games game = JsonConvert.DeserializeObject<Games>(fileText);
            return game;
        }
    }
}