using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    public interface IMenus
    {
        public string DisplayMenu();
        public int[] DisplayGameSelection(int numberOfGames);
        public void DisplayOutput(string text);
        public void DrawGrid(bool[,,] gameGrid, int iteration, int aliveCellCount, int height, int width, int aliveGridCount);
        public void DrawEightGrids(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int height, int width, int[] selectedGames, int aliveGridCount);
    }
}
