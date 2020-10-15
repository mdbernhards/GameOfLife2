using Newtonsoft.Json;

namespace GameOfLife
{
    /// <summary>
    /// Class for creating a Game object for saving and loading Game of Life information
    /// </summary>
    public class Games
    {
        public bool[,,] GameGrid { get; set; }
        public int Iteration { get; set; }
        public int[] AliveCellCount { get; set; }
        public int AliveGridCount { get; set; }
        public int Height { get;}
        public int Width { get;}

        /// <summary>
        /// Constructor to create the object with all the necessary information
        /// </summary>
        [JsonConstructor]
        public Games(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount)
        {
            GameGrid = gameGrid;
            Iteration = iteration;
            AliveCellCount = aliveCellCount;
            AliveGridCount = aliveGridCount;

            Height = gameGrid.GetLength(0);
            Width = gameGrid.GetLength(1);
        }
        public Games(bool[,,] gameGrid)
        {
            GameGrid = gameGrid;
        }
    }
}