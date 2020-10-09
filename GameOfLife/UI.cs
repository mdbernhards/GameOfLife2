using System;
using System.Text;

namespace GameOfLife
{
    public class UI
    {
        public static string DrawMenu()
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
    }
}
