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
        private readonly Mock<IMenus> MenusMock = new Mock<IMenus>();
        private readonly Mock<IGamePause> GamePauseMock = new Mock<IGamePause>();

        private Grid Grids { get; set; }
        private bool[,,] GameGrid { get; set; }

        private void SetUp()
        {
            MenusMock.Setup(menus => menus.DrawGrid(It.IsAny<bool[,,]>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            GamePauseMock.Setup(pause => pause.CheckForPauseOrSave(It.IsAny<Games>(), It.IsAny<Timer>())).Verifiable();

            Grids = new Grid(MenusMock.Object, GamePauseMock.Object, 1, 5, 5)
            {
                LastAliveCellCount = new int[1],
                Game = new Games(new bool[5, 5, 1], 0, new int[1], 0),
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
        public void UpdateGridUnitTestMovingBlinkerShape()
        {
            SetUp();

            GameGrid[1, 3, 0] = true;
            GameGrid[2, 3, 0] = true;
            GameGrid[3, 3, 0] = true;

            Array.Copy(GameGrid, Grids.Game.GameGrid, GameGrid.Length);
            Array.Copy(GameGrid, Grids.NextGameGrid, GameGrid.Length);

            Grids.UpdateGrid(null, null);

            GameGrid[1, 3, 0] = false;
            GameGrid[3, 3, 0] = false;
            GameGrid[2, 2, 0] = true;
            GameGrid[2, 4, 0] = true;

            Assert.Equal(GameGrid, Grids.NextGameGrid);
            Mock.VerifyAll();
        }

        [Fact]
        public void UpdateGridTestStaticCubeShape()
        {
            SetUp();

            GameGrid[2, 2, 0] = true;
            GameGrid[2, 3, 0] = true;
            GameGrid[3, 3, 0] = true;
            GameGrid[3, 2, 0] = true;

            Array.Copy(GameGrid, Grids.Game.GameGrid, GameGrid.Length);
            Array.Copy(GameGrid, Grids.NextGameGrid, GameGrid.Length);

            Grids.UpdateGrid(null, null);

            Assert.Equal(GameGrid, Grids.NextGameGrid);
            Mock.VerifyAll();
        }

        [Fact]
        public void GetAliveNeighborsUnitTest()
        {
            //Setup
            SetUp();

            GameGrid[2, 1, 0] = true;
            GameGrid[2, 3, 0] = true;
            GameGrid[3, 2, 0] = true;
            GameGrid[1, 2, 0] = true;

            Array.Copy(GameGrid, Grids.Game.GameGrid, GameGrid.Length);

            //Act
            int aliveNeighbors = Grids.GetAliveNeighbors(2, 2, 0);

            //Test
            Assert.Equal(4, aliveNeighbors);
        }
    }
}