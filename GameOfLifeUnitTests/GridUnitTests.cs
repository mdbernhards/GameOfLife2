using Moq;
using Xunit;
using GameOfLife;
using System;
using GameOfLife.Interfaces;
using System.Timers;

namespace GameOfLifeUnitTests
{
    /// <summary>
    /// Unit tests that test the Grid Class
    /// </summary>
    public class GridUnitTests
    {
        private readonly Mock<IMenus> MenusMock = new Mock<IMenus>();
        private readonly Mock<IGamePause> GamePauseMock = new Mock<IGamePause>();
        private Grid Grids;
        private bool[,,] GameGrid;

        /// <summary>
        /// Sets up needed variables and objects for Grid tests
        /// </summary>
        private void SetUp()
        {
            MenusMock.Setup(menus => menus.DrawGrid(It.IsAny<Games>()));
            GamePauseMock.Setup(pause => pause.CheckForPauseOrSave(It.IsAny<Games>(), It.IsAny<Timer>()));

            Games Game = new Games(new bool[5, 5, 1], 0, new int[1], 0);
            Grids = new Grid(MenusMock.Object, GamePauseMock.Object, 1, Game);

            GameGrid = new bool[5, 5, 1];
            for (int line = 0; line < GameGrid.GetLength(0); line++)
            {
                for (int character = 0; character < GameGrid.GetLength(1); character++)
                {
                    GameGrid[line, character, 0] = false;
                }
            }
        }

        /// <summary>
        /// Tests if UpdateGrid logic is working correctly on a shape that changes after update
        /// </summary>
        [Fact]
        public void UpdateGridUnitTestMovingBlinkerShape()
        {
            //Setup
            SetUp();

            GameGrid[1, 3, 0] = true;
            GameGrid[2, 3, 0] = true;
            GameGrid[3, 3, 0] = true;

            bool[,,] testGrid = new bool[5, 5, 1];

            Array.Copy(GameGrid, Grids.Game.GameGrid, GameGrid.Length);
            Array.Copy(GameGrid, Grids.NextGameGrid, GameGrid.Length);
            Array.Copy(GameGrid, testGrid, GameGrid.Length);

            testGrid[1, 3, 0] = false;
            testGrid[3, 3, 0] = false;
            testGrid[2, 2, 0] = true;
            testGrid[2, 4, 0] = true;

            //Act
            Grids.UpdateGrid(null, null);


            //Test
            Assert.Equal(testGrid, Grids.NextGameGrid);
        }

        /// <summary>
        /// Tests if UpdateGrid logic is working correctly on a shape that doesn't change after update
        /// </summary>
        [Fact]
        public void UpdateGridTestStaticCubeShape()
        {
            //Setup
            SetUp();

            GameGrid[2, 2, 0] = true;
            GameGrid[2, 3, 0] = true;
            GameGrid[3, 3, 0] = true;
            GameGrid[3, 2, 0] = true;

            Array.Copy(GameGrid, Grids.Game.GameGrid, GameGrid.Length);
            Array.Copy(GameGrid, Grids.NextGameGrid, GameGrid.Length);

            //Act
            Grids.UpdateGrid(null, null);

            //Test
            Assert.Equal(GameGrid, Grids.NextGameGrid);
        }

        /// <summary>
        /// Tests if GetAliveNeighbors correctly counts neighbors of a cell
        /// </summary>
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