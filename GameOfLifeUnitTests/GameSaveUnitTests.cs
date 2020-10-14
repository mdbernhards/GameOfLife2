using Moq;
using System;
using Xunit;
using GameOfLife;

namespace GameOfLifeUnitTests
{
    public class GameSaveUnitTests
    {
        [Fact]
        public void LoadGameUnitTest()
        {
            Grid grid = new Grid();

            bool[,,] GameGrid = new bool[5, 5, 1];
            int[] aliveCellCount = new int[1];
            aliveCellCount[0] = 20;

            Game game = new Game(GameGrid, 3, aliveCellCount, 1);

            var gameSaveMock = new Mock<IGameSave>();
            //gameSaveMock.Setup(save => save.ReadSaveFile()).Returns(game);

            //var saveInfo = gameSaveMock.ReadSaveFile();
            //grid.CreateGridFromFile(saveInfo.GameGrid, saveInfo.Iteration, saveInfo.AliveCellCount, saveInfo.AliveGridCount);

            gameSaveMock.Setup(save => save.ReadSaveFile()).Returns(game);
        }
    }
}