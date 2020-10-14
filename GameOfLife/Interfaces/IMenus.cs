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
        public void DrawARowOfGrids(bool[,,] gameGrid, int height, int width, int[] selectedGames, StringBuilder grids, int gridRow);
        public void DrawGridTitles(bool[,,] gameGrid, int[] aliveCellCount, int[] selectedGames, StringBuilder grids, int gridLine);
    }
}