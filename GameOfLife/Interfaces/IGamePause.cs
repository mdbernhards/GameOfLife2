using System.Timers;

namespace GameOfLife.Interfaces
{
    public interface IGamePause
    {
        public void CheckForPauseOrSave(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount, Timer timer);
    }
}