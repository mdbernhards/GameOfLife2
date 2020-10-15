using System;
using System.Linq;
using System.Text;

namespace GameOfLife
{

    /// <summary>
    /// Class that displays or draws UI, methods are called when there is need for program output
    /// </summary>
    public class Menus : Interfaces.IMenus
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
        /// <param name="numberOfGames"> Number of games available and you can choose</param>
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
        /// <param name="text"> Text thats going to be displayed on screen</param>
        public void DisplayOutput(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Draws the grid every time it has been updated
        /// </summary>
        public void DrawGrid(Games game)
        {
            Console.Clear();

            StringBuilder gridString = new StringBuilder();

            gridString.AppendLine("Iteration: " + game.Iteration);
            gridString.AppendLine("Alive cell count: " + game.AliveCellCount.Sum());
            gridString.AppendLine("Alive Grid count: " + game.AliveGridCount);
            gridString.AppendLine("");
            gridString.AppendLine("Press Space to pause and unpause");
            gridString.AppendLine("While Paused press S to save");
            gridString.AppendLine("");

            for (int line = 0; line < game.Height; line++)
            {
                for (int character = 0; character < game.Width; character++)
                {
                    if (game.GameGrid[line, character, 0] == true)
                    {
                        gridString.Append("█");
                    }
                    else
                    {
                        gridString.Append(" ");
                    }
                }

                gridString.AppendLine("");
            }

            Console.WriteLine(gridString);
        }

        /// <summary>
        /// Draws eight grids every time they have been updated
        /// </summary>
        public void DrawEightGrids(Games game, int[] selectedGames)
        {
            Console.Clear();

            StringBuilder gridsString = new StringBuilder();

            gridsString.AppendLine("Iteration: " + game.Iteration);
            gridsString.AppendLine("Total alive cell count: " + game.AliveCellCount.Sum());
            gridsString.AppendLine("Alive Grid count: " + game.AliveGridCount);
            gridsString.AppendLine("");
            gridsString.AppendLine("Press Space to pause and unpause");
            gridsString.AppendLine("While Paused press S to save");
            gridsString.AppendLine("");

            for (int gridRow = 0; gridRow < 2; gridRow++)
            {
                DrawGridTitles(game, selectedGames, gridsString, gridRow);
                DrawARowOfGrids(game, selectedGames, gridsString, gridRow);
            }

            Console.WriteLine(gridsString);
        }

        /// <summary>
        /// Draws a row of 4 grids
        /// </summary>
        public void DrawARowOfGrids(Games game, int[] selectedGames, StringBuilder grids, int gridRow)
        {
            for (int line = 0; line < game.Height; line++)
            {
                for (int gridColumn = 0; gridColumn < 4; gridColumn++)
                {
                    if (gridRow == 1)
                    {
                        gridColumn += 4;
                    }

                    for (int character = 0; character < game.Width; character++)
                    {
                        if (game.GameGrid[line, character, selectedGames[gridColumn]])
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
        public void DrawGridTitles(Games game, int[] selectedGames, StringBuilder grids, int gridLine)
        {
            for (int title = 0; title < 4; title++)
            {
                if (gridLine == 0)
                {
                    grids.Append(" Game: " + (selectedGames[title] + 1) + "  Alive cell count: " + game.AliveCellCount[selectedGames[title]] + new string(' ', game.GameGrid.GetLength(1) - 27));
                }
                else
                {
                    grids.Append(" Game: " + (selectedGames[title + 4] + 1) + "  Alive cell count: " + game.AliveCellCount[selectedGames[title + 4]] + new string(' ', game.GameGrid.GetLength(1) - 27));
                }
            }

            grids.AppendLine("");
        }
    }
}