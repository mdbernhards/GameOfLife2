namespace GameOfLife
{
    /// <summary>
    /// Starts the program, menu
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            GameManager menu = new GameManager();
            menu.StartTheMenu();
        }
    }
}