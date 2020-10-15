using GameOfLife.Interfaces;
using System;

namespace GameOfLife
{
    /// <summary>
    /// Has the Start menu logic
    /// </summary>
    public class GameManager
    {
        private GameSave gameSave { get; set; }
        private IMenus menus { get; set; }
        private Grid grid { get; set; }

        /// <summary>
        /// Enum for menu
        /// </summary>
        enum MenuItems 
        { 
            SmallGrid = 1,
            BigGrid = 2,
            CustomGrid = 3,
            ThousandGrids = 4,
            LoadFromFile = 5,
            Exit = 6
        };

        /// <summary>
        /// Constuctor that creates objects for GameManager class
        /// </summary>
        public GameManager()
        {
            gameSave = new GameSave();
            menus = new Menus();
            grid = new Grid();
        }

        /// <summary>
        /// Creates the start menu, calls a method that asks for user input
        /// </summary>
        public void StartTheMenu()
        {
            string menuNuber = menus.DisplayMenu();
            Enum.TryParse(menuNuber, result: out MenuItems menuItems);

            switch (menuItems)
            {
                case MenuItems.SmallGrid:
                    grid = new Grid(30, 60, 1);
                    grid.CreateGrids(); //height, width, number of games in paralel
                    break;
                case MenuItems.BigGrid:
                    grid = new Grid(40, 80, 1);
                    grid.CreateGrids();
                    break;
                case MenuItems.CustomGrid:
                    grid = new Grid(30, 30, 1);
                    grid.CreateCustomGrid();
                    break;
                case MenuItems.ThousandGrids:
                    grid = new Grid(18, 40, 1000);
                    grid.CreateGrids();
                    break;
                case MenuItems.LoadFromFile:
                    grid = new Grid();

                    var saveInfo = gameSave.ReadSaveFile();
                    grid.CreateGridFromFile(saveInfo.GameGrid, saveInfo.Iteration, saveInfo.AliveCellCount, saveInfo.AliveGridCount);
                    break;
                case MenuItems.Exit:
                    break;
                default:
                    StartTheMenu();
                    break;
            }
        }
    }
}