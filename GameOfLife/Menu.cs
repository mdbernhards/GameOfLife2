using System;

namespace GameOfLife
{
    public class Menu
    {
        public void StartMenu()
        {
            Console.WriteLine("Hello to Game of Life!");
            Console.WriteLine(" ");
            Console.WriteLine("Choose the size of field, by typing a number:");
            Console.WriteLine("1. 30 X 60");
            Console.WriteLine("2. 40 X 80");
            Console.WriteLine("3. 50 X 100");
            Console.WriteLine("4. Custom test grid");
            Console.WriteLine("5. Exit");

            string menuNuber = Console.ReadLine();
            Grid grid = new Grid();

            switch (menuNuber)
            {
                case "1":
                    grid.CreateGrid(30, 60);
                    break;
                case "2":
                    grid.CreateGrid(40, 80);
                    break;
                case "3":
                    grid.CreateGrid(50, 100);
                    break;
                case "4":
                    grid.CustomGrid(30, 30);
                    break;
                case "5":
                    break;
                default:
                    break;
            }
        }
    }
}
