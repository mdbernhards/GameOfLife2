namespace GameOfLife
{
    /// <summary>
    /// Interface for class that saves and loads Game of Life
    /// </summary>
    public interface IGameSave
    {
        public Game ReadSaveFile();
        public void SaveGame(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount);
    }
}