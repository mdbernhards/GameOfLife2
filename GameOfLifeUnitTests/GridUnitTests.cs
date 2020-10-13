using Moq;
using Xunit;
using GameOfLife;
using GameOfLife.Interfaces;
using System;

namespace GameOfLifeUnitTests
{
    public class GridUnitTests
    {
        [Fact]
        public void UpdateGridTest()
        {
            Grid grid = new Grid();

            grid.AliveCellCount = new int[1];
            grid.LastAliveCellCount = new int[1];

            var menusMock = new Mock<Menus>();
            menusMock.CallBase = true;
            menusMock.Setup(menus => menus.DrawGrid(It.IsAny<bool[,,]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            bool[,,] gameGrid = new bool[5, 5, 1];
            grid.GameGrid = new bool[5, 5, 1];
            grid.NextGameGrid = new bool[5, 5, 1];

            for (int line = 0; line < gameGrid.GetLength(0); line++)
            {
                for (int character = 0; character < gameGrid.GetLength(1); character++)
                {
                    gameGrid[line, character, 0] = false;
                }
            }

            gameGrid[1, 3, 0] = true;
            gameGrid[2, 3, 0] = true;
            gameGrid[3, 3, 0] = true;

            Array.Copy(gameGrid, grid.GameGrid, gameGrid.Length);
            Array.Copy(gameGrid, grid.NextGameGrid, gameGrid.Length);


            grid.UpdateGrid(null, null);
            menusMock.Verify();

            Assert.Equal(gameGrid, grid.GameGrid);
        }
    }
}
