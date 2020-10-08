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

        public Game(bool[,,] gameGrid, int iteration, int[] aliveCellCount)
        {
            GameGrid = gameGrid;
            Iteration = iteration;
            AliveCellCount = aliveCellCount;
        }
    }
}
