namespace GameOfLife
{
    /// <summary>
    /// Has the Start menu logic
    /// </summary>
    public class GameManager
    {
        private GameSave gameSave { get; set; }
        private Menus menus { get; set; }
        private Grid grid { get; set; }

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

            switch (menuNuber)
            {
                case "1":
                    grid.CreateGrids(30, 60, 1); //height, width, number of games in paralel
                    break;
                case "2":
                    grid.CreateGrids(40, 80, 1);
                    break;
                case "3":
                    grid.CreateCustomGrid(30, 30);
                    break;
                case "4":
                    grid.CreateGrids(18, 40, 1000);
                    break;
                case "5":
                    var saveInfo = gameSave.ReadSaveFile();
                    grid.CreateGridFromFile(saveInfo.GameGrid, saveInfo.Iteration, saveInfo.AliveCellCount, saveInfo.AliveGridCount);
                    break;
                case "6":
                    break;
                default:
                    StartTheMenu();
                    break;
            }
        }
    }
}