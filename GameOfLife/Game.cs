using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Game
    {
        public bool[,,] GameGrid { get; set; }
        public int Iteration { get; set; }
        public int[] AliveCellCount { get; set; }

        public int AliveGridCount { get; set; }

        public Game(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount)
        {
            GameGrid = gameGrid;
            Iteration = iteration;
            AliveCellCount = aliveCellCount;
            AliveGridCount = aliveGridCount;
        }
    }
}
