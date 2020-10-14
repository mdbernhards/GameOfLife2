using Xunit;
using GameOfLife;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;

namespace GameOfLifeUnitTests
{
    public class GameSaveUnitTests
    {
        private bool[,,] Grid;
        private Game TestGame;

        private void SetUp()
        {
            Grid = new bool[5, 5, 1];

            for (int line = 0; line < Grid.GetLength(0); line++)
            {
                for (int character = 0; character < Grid.GetLength(1); character++)
                {
                    Grid[line, character, 0] = false;
                }
            }

            Grid[1, 3, 0] = true;
            Grid[2, 3, 0] = true;
            Grid[3, 3, 0] = true;

            int[] aliveCellCount = { 3 };
            TestGame = new Game(Grid, 2, aliveCellCount, 1);
        }

        [Fact]
        public void LoadGameUnitTest()
        {
            string jsonData = "{'GameGrid':[[[false],[false],[false],[false],[false]],[[false],[false],[false],[true],[false]],[[false],[false],[false],[true],[false]],[[false],[false],[false],[true],[false]],[[false],[false],[false],[false],[false]]],'Iteration':2,'AliveCellCount':[3],'AliveGridCount':1}  ";  

            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { "Saves/GameSave.json", new MockFileData(jsonData) }
            });

            var gameSave = new GameSave(fileSystem);
            var game = gameSave.ReadSaveFile();

            SetUp();

            Assert.Equal(game.GameGrid, TestGame.GameGrid);
            Assert.Equal(game.Iteration, TestGame.Iteration);
            Assert.Equal(game.AliveCellCount, TestGame.AliveCellCount);
            Assert.Equal(game.AliveGridCount, TestGame.AliveGridCount);
        }
    }
}