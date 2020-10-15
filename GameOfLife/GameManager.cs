using GameOfLife.Enums;
using GameOfLife.Interfaces;
using System;

namespace GameOfLife
{
    /// <summary>
    /// Has the Start menu logic
    /// </summary>
    public class GameManager
    {
        private readonly GameSave gameSave;
        private readonly IMenus menus;
        private Grid grid;

        /// <summary>
        /// Constuctor that creates gameSave, menus and grid objects
        /// </summary>
        public GameManager()
        {
            gameSave = new GameSave();
            menus = new Menus();
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
                    grid = new Grid(30, 60, 1); //height, width, number of games in paralel
                    grid.CreateGrids(); 
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
                    
                    var saveInfo = gameSave.ReadSaveFile();
                    grid = new Grid(saveInfo);

                    grid.CreateGridFromFile();
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