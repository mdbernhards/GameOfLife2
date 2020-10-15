using System.Timers;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// Interface for class for pausing and saving
    /// </summary>
    public interface IGamePause
    {
        /// <summary>
        /// Checks if Game of Life needs to be: paused, unpaused or saved
        /// </summary>
        /// <param name="game"> Game object that stores information about game grid</param>
        /// <param name="timer"> Timer object that can be stoped and started up again</param>
        public void CheckForPauseOrSave(Games game, Timer timer);
    }
}