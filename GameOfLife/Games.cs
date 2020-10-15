using Newtonsoft.Json;

namespace GameOfLife
{
    /// <summary>
    /// Class for creating a Game object for saving and loading Game of Life information
    /// </summary>
    public class Games
    {
        /// <summary>
        /// All grids are stored in this array
        /// </summary>
        public bool[,,] GameGrid { get; set; }

        /// <summary>
        /// How many times have the grids been updated
        /// </summary>
        public int Iteration { get; set; }

        /// <summary>
        /// How many alive cells each grid has
        /// </summary>
        public int[] AliveCellCount { get; set; }

        /// <summary>
        /// How many grids are alive and moving
        /// </summary>
        public int AliveGridCount { get; set; }

        /// <summary>
        /// Height of grids
        /// </summary>
        public int Height { get;}

        /// <summary>
        /// Width of grids
        /// </summary>
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