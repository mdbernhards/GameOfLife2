namespace GameOfLife
{
    /// <summary>
    /// Main programs class, starts the programs menu
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the program, by creating a menu object and displaying it
        /// </summary>
        public static void Main(string[] args)
        {
            GameManager menu = new GameManager();
            menu.StartTheMenu();
        }
    }
}