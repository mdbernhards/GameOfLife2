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

        enum MenuItems 
        { 
            SmallGrid = 1,
            BigGrid = 2,
            CustomGrid = 3,
            ThousandGrids = 4,
            LoadFromFile = 5,
            Exit = 6
        };

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
                    grid.CreateGrids(30, 60, 1); //height, width, number of games in paralel
                    break;
                case MenuItems.BigGrid:
                    grid.CreateGrids(40, 80, 1);
                    break;
                case MenuItems.CustomGrid:
                    grid.CreateCustomGrid(30, 30);
                    break;
                case MenuItems.ThousandGrids:
                    grid.CreateGrids(18, 40, 1000);
                    break;
                case MenuItems.LoadFromFile:
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