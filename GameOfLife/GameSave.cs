﻿using Newtonsoft.Json;
using System.IO;

namespace GameOfLife
{
    /// <summary>
    /// Saves and loads saved Game of Life state
    /// </summary>
    public class GameSave : IGameSave
    {
        public const string filePath = "Saves/GameSave.json";
        public const string folderPath = "Saves/";

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
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filePath));
            return game;
        }
    }
}