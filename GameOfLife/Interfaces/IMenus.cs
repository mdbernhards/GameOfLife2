using System.Text;

namespace GameOfLife.Interfaces
{
    /// <summary>
    /// Interface for class that displays or draws UI, methods are called when there is need for program output
    /// </summary>
    public interface IMenus
    {
        /// <summary>
        /// Displays the game selection menu asks to enter one of the menu items and returns it
        /// </summary>
        public string DisplayMenu();

        /// <summary>
        /// UI that lets you select which grids to show, returns the grid "id's"
        /// </summary>
        /// <param name="numberOfGames"> Number of games available and you can choose</param>
        public int[] DisplayGameSelection(int numberOfGames);

        /// <summary>
        /// Displays the given string
        /// </summary>
        /// <param name="text"> Text thats going to be displayed on screen</param>
        public void DisplayOutput(string text);
        public void DrawGrid(Games game);
        public void DrawEightGrids(Games game, int[] selectedGames);
        public void DrawARowOfGrids(Games game, int[] selectedGames, StringBuilder grids, int gridRow);
        public void DrawGridTitles(Games game, int[] selectedGames, StringBuilder grids, int gridLine);
    }
}