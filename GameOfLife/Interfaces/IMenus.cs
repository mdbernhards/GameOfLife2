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
        /// <param name="numberOfGames">Number of games available and you can choose</param>
        public int[] DisplayGameSelection(int numberOfGames);

        /// <summary>
        /// Displays the given string
        /// </summary>
        /// <param name="text">Text thats going to be displayed on screen</param>
        public void DisplayOutput(string text);

        /// <summary>
        /// Draws the grid every time it has been updated
        /// </summary>
        /// <param name="game">Game object that has information of the grids that need to be drawn</param>
        public void DrawGrid(Games game);

        /// <summary>
        /// Draws eight grids every time they have been updated
        /// </summary>
        /// <param name="game">Game object that has information of the grids that need to be drawn</param>
        /// <param name="selectedGames">8 Id's of games selected to be drawn</param>
        public void DrawEightGrids(Games game, int[] selectedGames);

        /// <summary>
        /// Draws a row of 4 grids
        /// </summary>
        /// <param name="game">Game object that has information of the grids that need to be drawn</param>
        /// <param name="selectedGames">8 Id's of games selected to be drawn</param>
        /// <param name="grids">StringBuilder that a row of grids will be added</param>
        /// <param name="gridRow">What row of grids needs to be added to StringBuilder</param>
        public void DrawARowOfGrids(Games game, int[] selectedGames, StringBuilder grids, int gridRow);

        /// <summary>
        /// Draws the name and cell count of each shown grid
        /// </summary>
        /// <param name="game">Game object that has information of the grids that need to be drawn</param>
        /// <param name="selectedGames">8 Id's of games selected to be drawn</param>
        /// <param name="grids">StringBuilder that idividual grid statistics will be added</param>
        /// <param name="gridRow">What line will the statistics be added</param>
        public void DrawGridTitles(Games game, int[] selectedGames, StringBuilder grids, int gridLine);
    }
}