using Moq;
using Xunit;
using GameOfLife;
using System;
using GameOfLife.Interfaces;
using System.Timers;

namespace GameOfLifeUnitTests
{
    public class GridUnitTests
    {
        public Mock<IMenus> menusMock = new Mock<IMenus>();
        public Mock<IGamePause> gamePauseMock = new Mock<IGamePause>();

        [Fact]
        public void UpdateGridTestMovingShape()
        {
            menusMock.Setup(menus => menus.DrawGrid(It.IsAny<bool[,,]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            gamePauseMock.Setup(pause => pause.CheckForPauseOrSave(It.IsAny<bool[,,]>(), It.IsAny<int>(), It.IsAny<int[]>(), It.IsAny<int>(), It.IsAny<Timer>()));

            var grid = new Grid(menusMock.Object, gamePauseMock.Object, 1, 5, 5);
            grid.AliveCellCount = new int[1];
            grid.LastAliveCellCount = new int[1];
            //grid

            grid.GameGrid = new bool[5, 5, 1];
            grid.NextGameGrid = new bool[5, 5, 1];
            bool[,,] gameGrid = new bool[5, 5, 1];

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

            gameGrid[1, 3, 0] = false;
            gameGrid[3, 3, 0] = false;
            gameGrid[2, 2, 0] = true;
            gameGrid[2, 4, 0] = true;

            Assert.Equal(gameGrid, grid.NextGameGrid);
            Mock.VerifyAll();
        }

        public void UpdateGridTestStaticShape()
        {
            menusMock.Setup(menus => menus.DrawGrid(It.IsAny<bool[,,]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            gamePauseMock.Setup(pause => pause.CheckForPauseOrSave(It.IsAny<bool[,,]>(), It.IsAny<int>(), It.IsAny<int[]>(), It.IsAny<int>(), It.IsAny<Timer>()));

            var grid = new Grid(menusMock.Object, gamePauseMock.Object, 1, 5, 5);
            grid.AliveCellCount = new int[1];
            grid.LastAliveCellCount = new int[1];
            //grid

            grid.GameGrid = new bool[5, 5, 1];
            grid.NextGameGrid = new bool[5, 5, 1];
            bool[,,] gameGrid = new bool[5, 5, 1];

            for (int line = 0; line < gameGrid.GetLength(0); line++)
            {
                for (int character = 0; character < gameGrid.GetLength(1); character++)
                {
                    gameGrid[line, character, 0] = false;
                }
            }

            gameGrid[2, 2, 0] = true;
            gameGrid[2, 3, 0] = true;
            gameGrid[3, 3, 0] = true;
            gameGrid[3, 2, 0] = true;

            Array.Copy(gameGrid, grid.GameGrid, gameGrid.Length);
            Array.Copy(gameGrid, grid.NextGameGrid, gameGrid.Length);

            grid.UpdateGrid(null, null);

            Assert.Equal(gameGrid, grid.NextGameGrid);
            Mock.VerifyAll();
        }
    }
}