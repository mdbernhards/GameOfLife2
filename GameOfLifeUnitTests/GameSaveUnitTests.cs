using Xunit;
using GameOfLife;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameOfLifeUnitTests
{
    public class GameSaveUnitTests
    {
        private bool[,,] Grid;
        private Games TestGame;

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
            TestGame = new Games(Grid, 2, aliveCellCount, 1);
        }

        [Fact]
        public void LoadGameUnitTestJsonDataDeserialization()
        {
            //Setup
            SetUp();

            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { "Saves/GameSave.json", new MockFileData(JsonConvert.SerializeObject(TestGame)) }
            });

            var gameSave = new GameSave(fileSystem);

            //Act
            var game = gameSave.ReadSaveFile();

            //Test
            Assert.Equal(game.GameGrid, TestGame.GameGrid);
            Assert.Equal(game.Iteration, TestGame.Iteration);
            Assert.Equal(game.AliveCellCount, TestGame.AliveCellCount);
            Assert.Equal(game.AliveGridCount, TestGame.AliveGridCount);
        }
    }
}