namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(false, false);
            game.GameInit();
        }
    }
}