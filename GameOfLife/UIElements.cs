using System;

namespace GameOfLife
{
    //Creates and draws all UI elements
    public class UIElements
    {
        //Creates the start menu
        public void StartMenu()
        {
            Console.WriteLine("Hello to Game of Life!");
            Console.WriteLine(" ");
            Console.WriteLine("Choose the size of field, by typing a number:");
            Console.WriteLine("1. 30 X 60");
            Console.WriteLine("2. 40 X 80");
            Console.WriteLine("3. Custom test grid");
            Console.WriteLine("4. Load last saved game");
            Console.WriteLine("5. Exit");

            string menuNuber = Console.ReadLine();
            Grid grid = new Grid();
            GameSave gameSave = new GameSave();

            switch (menuNuber)
            {
                case "1":
                    grid.CreateGrid(30, 60);
                    break;
                case "2":
                    grid.CreateGrid(40, 80);
                    break;
                case "3":
                    grid.CreateCustomGrid(30, 30);
                    break;
                case "4":
                    gameSave.LoadSave();
                    break;
                case "5":
                    break;
            }
        }
    }
}