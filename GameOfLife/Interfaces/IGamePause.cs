using System.Timers;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// Interface for class for pausing and saving
    /// </summary>
    public interface IGamePause
    {
        public void CheckForPauseOrSave(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount, Timer timer);
    }
}