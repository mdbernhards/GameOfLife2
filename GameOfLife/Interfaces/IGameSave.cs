using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public interface IGameSave
    {
        public Game ReadSaveFile();
        public void SaveGame(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int aliveGridCount);
    }
}
