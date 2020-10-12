namespace GameOfLife
{
    /// <summary>
    /// Class for creating a Game object for saving and loading Game of Life information
    /// </summary>
    public class Game
    {
        public bool[,,] GameGrid { get; set; }
        public int Iteration { get; set; }
        public int[] AliveCellCount { get; set; }
        public int AliveGridCount { get; set; }

        /// <summary>
        /// Constructor to create the object with all the necessary information
        /// </summary>
        public Game(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount)
        {
            GameGrid = gameGrid;
            Iteration = iteration;
            AliveCellCount = aliveCellCount;
            AliveGridCount = aliveGridCount;
        }
    }
}