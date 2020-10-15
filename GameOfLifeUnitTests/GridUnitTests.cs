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
        private Mock<IMenus> menusMock = new Mock<IMenus>();
        private Mock<IGamePause> gamePauseMock = new Mock<IGamePause>();

        private Grid grid;
        private bool[,,] GameGrid;

        private void SetUp()
        {
            menusMock.Setup(menus => menus.DrawGrid(It.IsAny<bool[,,]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            gamePauseMock.Setup(pause => pause.CheckForPauseOrSave(It.IsAny<bool[,,]>(), It.IsAny<int>(), It.IsAny<int[]>(), It.IsAny<int>(), It.IsAny<Timer>())).Verifiable();

            grid = new Grid(menusMock.Object, gamePauseMock.Object, 1, 5, 5)
            {
                AliveCellCount = new int[1],
                LastAliveCellCount = new int[1],
                GameGrid = new bool[5, 5, 1],
                NextGameGrid = new bool[5, 5, 1]
            };

            GameGrid = new bool[5, 5, 1];
            for (int line = 0; line < GameGrid.GetLength(0); line++)
            {
                for (int character = 0; character < GameGrid.GetLength(1); character++)
                {
                    GameGrid[line, character, 0] = false;
                }
            }
        }

        [Fact]
        public void UpdateGridTestMovingShape()
        {
            SetUp();

            GameGrid[1, 3, 0] = true;
            GameGrid[2, 3, 0] = true;
            GameGrid[3, 3, 0] = true;

            Array.Copy(GameGrid, grid.GameGrid, GameGrid.Length);
            Array.Copy(GameGrid, grid.NextGameGrid, GameGrid.Length);

            grid.UpdateGrid(null, null);

            GameGrid[1, 3, 0] = false;
            GameGrid[3, 3, 0] = false;
            GameGrid[2, 2, 0] = true;
            GameGrid[2, 4, 0] = true;

            Assert.Equal(GameGrid, grid.NextGameGrid);
            Mock.VerifyAll();
        }

        [Fact]
        public void UpdateGridTestStaticShape()
        {
            SetUp();

            GameGrid[2, 2, 0] = true;
            GameGrid[2, 3, 0] = true;
            GameGrid[3, 3, 0] = true;
            GameGrid[3, 2, 0] = true;

            Array.Copy(GameGrid, grid.GameGrid, GameGrid.Length);
            Array.Copy(GameGrid, grid.NextGameGrid, GameGrid.Length);

            grid.UpdateGrid(null, null);

            Assert.Equal(GameGrid, grid.NextGameGrid);
            Mock.VerifyAll();
        }

        [Fact]
        public void GetAliveNeighborsUnitTest()
        {
            SetUp();

            GameGrid[2, 1, 0] = true;
            GameGrid[2, 3, 0] = true;
            GameGrid[3, 2, 0] = true;
            GameGrid[1, 2, 0] = true;

            Array.Copy(GameGrid, grid.GameGrid, GameGrid.Length);
            Array.Copy(GameGrid, grid.NextGameGrid, GameGrid.Length);

            int aliveNeighbors = grid.GetAliveNeighbors(2, 2, 0);

            Assert.Equal(4, aliveNeighbors);
        }
    }
}