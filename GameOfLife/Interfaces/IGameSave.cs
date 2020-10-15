namespace GameOfLife
{
    /// <summary>
    /// Interface for class that saves and loads Game of Life
    /// </summary>
    public interface IGameSave
    {
        /// <summary>
        /// Saves game state when called
        /// </summary>
        /// <param name="game">Game object that stores gameGrid, aliveCellCount, aliveGridCount and iteration</param>
        public void SaveGame(Games game);

        /// <summary>
        /// Loads and returns saved game state when called
        /// </summary>
        public Games ReadSaveFile();
    }
}