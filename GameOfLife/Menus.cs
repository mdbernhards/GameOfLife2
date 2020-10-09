using System;
using System.Text;

namespace GameOfLife
{
    public class Menus
    {
        /// <summary>
        /// Displays the game selection menu asks to enter one of the menu items and returns it
        /// </summary>
        public static string DisplayMenu()
        {
            Console.Clear();
            StringBuilder menu = new StringBuilder();

            menu.AppendLine("Hello to Game of Life!");
            menu.AppendLine(" ");
            menu.AppendLine("Choose the size of field, by typing a number:");
            menu.AppendLine("1. 30 X 60");
            menu.AppendLine("2. 40 X 80");
            menu.AppendLine("3. Custom test grid");
            menu.AppendLine("4. 1000 games in paralel");
            menu.AppendLine("5. Load last saved game");
            menu.AppendLine("6. Exit");
            Console.WriteLine(menu);

            Console.Write("Enter number: ");
            string menuNuber = Console.ReadLine();
            return menuNuber;
        }

        /// <summary>
        /// UI that lets you select which grids to show, returns the grid "id's"
        /// </summary>
        public static int[] DisplayGameSelection(int numberOfGames)
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
    }
}