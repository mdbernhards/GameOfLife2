using System;
using System.Linq;
using System.Text;

namespace GameOfLife
{

    /// <summary>
    /// Class for displaying or drawing UI, methods are called when there is need for program output
    /// </summary>
    public class Menus
    {
        /// <summary>
        /// Displays the game selection menu asks to enter one of the menu items and returns it
        /// </summary>
        public string DisplayMenu()
        {
            Console.Clear();
            StringBuilder menuText = new StringBuilder();

            menuText.AppendLine("Hello to Game of Life!");
            menuText.AppendLine(" ");
            menuText.AppendLine("Choose the size of field, by typing a number:");
            menuText.AppendLine("1. 30 X 60");
            menuText.AppendLine("2. 40 X 80");
            menuText.AppendLine("3. Custom test grid");
            menuText.AppendLine("4. 1000 games in paralel");
            menuText.AppendLine("5. Load last saved game");
            menuText.AppendLine("6. Exit");
            Console.WriteLine(menuText);

            Console.Write("Enter number: ");
            string menuNuber = Console.ReadLine();
            return menuNuber;
        }

        /// <summary>
        /// UI that lets you select which grids to show, returns the grid "id's"
        /// </summary>
        public int[] DisplayGameSelection(int numberOfGames)
        {
            int[] selectedGames = new int[8];

            Console.Clear();
            Console.WriteLine("Choose Games to show from 1 to " + numberOfGames);

            for (int gameNumber = 0; gameNumber < 8; gameNumber++)
            {
                Console.Write("Game No. " + (gameNumber + 1) + ": ");

                string number = Console.ReadLine();

                if (int.TryParse(number, out int numberValue))
                {
                    if (numberValue >= 1 && numberValue <= 1000)
                    {
                        selectedGames[gameNumber] = numberValue - 1;
                    }
                    else
                    {
                        gameNumber--;
                    }
                }
                else
                {
                    gameNumber--;
                }
            }

            return selectedGames;
        }

        /// <summary>
        /// Displays the given string
        /// </summary>
        public void DisplayOutput(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Draws the grid every time it has been updated
        /// </summary>
        public void DrawGrid(bool[,,] gameGrid, int iteration, int aliveCellCount, int height, int width, int aliveGridCount)
        {
            Console.Clear();

            StringBuilder grid = new StringBuilder();

            grid.AppendLine("Iteration: " + iteration);
            grid.AppendLine("Alive cell count: " + aliveCellCount);
            grid.AppendLine("Alive Grid count: " + aliveGridCount);
            grid.AppendLine("");
            grid.AppendLine("Press Space to pause and unpause");
            grid.AppendLine("While Paused press S to save");
            grid.AppendLine("");

            for (int line = 0; line < height; line++)
            {
                for (int character = 0; character < width; character++)
                {
                    if (gameGrid[line, character, 0] == true)
                    {
                        grid.Append("█");
                    }
                    else
                    {
                        grid.Append(" ");
                    }
                }

                grid.AppendLine("");
            }

            Console.WriteLine(grid);
        }

        /// <summary>
        /// Draws eight grids every time they have been updated
        /// </summary>
        public void DrawEightGrids(bool[,,] gameGrid, int iteration, int[] aliveCellCount, int height, int width, int[] selectedGames, int aliveGridCount)
        {
            Console.Clear();

            StringBuilder grids = new StringBuilder();

            grids.AppendLine("Iteration: " + iteration);
            grids.AppendLine("Total alive cell count: " + aliveCellCount.Sum());
            grids.AppendLine("Alive Grid count: " + aliveGridCount);
            grids.AppendLine("");
            grids.AppendLine("Press Space to pause and unpause");
            grids.AppendLine("While Paused press S to save");
            grids.AppendLine("");

            for (int gridRow = 0; gridRow < 2; gridRow++)
            {
                DrawGridTitles(gameGrid, aliveCellCount, selectedGames, grids, gridRow);
                DrawARowOfGrids(gameGrid, height, width, selectedGames, grids, gridRow);
            }

            Console.WriteLine(grids);
        }

        /// <summary>
        /// Draws a row of 4 grids
        /// </summary>
        private void DrawARowOfGrids(bool[,,] gameGrid, int height, int width, int[] selectedGames, StringBuilder grids, int gridRow)
        {
            for (int line = 0; line < height; line++)
            {
                for (int gridColumn = 0; gridColumn < 4; gridColumn++)
                {
                    if (gridRow == 1)
                    {
                        gridColumn += 4;
                    }

                    for (int character = 0; character < width; character++)
                    {
                        if (gameGrid[line, character, selectedGames[gridColumn]] == true)
                        {
                            grids.Append("█");
                        }
                        else
                        {
                            grids.Append(" ");
                        }
                    }

                    if (gridRow == 1)
                    {
                        gridColumn -= 4;
                    }

                    grids.Append(" || ");
                }

                grids.AppendLine("");
            }

            grids.AppendLine("");
        }

        /// <summary>
        /// Draws the name and cell count of each shown grid
        /// </summary>
        private void DrawGridTitles(bool[,,] gameGrid, int[] aliveCellCount, int[] selectedGames, StringBuilder grids, int gridLine)
        {
            for (int game = 0; game < 4; game++)
            {
                if (gridLine == 0)
                {
                    grids.Append(" Game: " + (selectedGames[game] + 1) + "  Alive cell count: " + aliveCellCount[selectedGames[game]] + new string(' ', gameGrid.GetLength(1) - 27));
                }
                else
                {
                    grids.Append(" Game: " + (selectedGames[game + 4] + 1) + "  Alive cell count: " + aliveCellCount[selectedGames[game + 4]] + new string(' ', gameGrid.GetLength(1) - 27));
                }
            }

            grids.AppendLine("");
        }
    }
}