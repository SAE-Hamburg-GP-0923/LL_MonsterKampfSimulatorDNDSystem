namespace LL_MonsterKampfSimulatorDNDSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //first bool is for debug, second bool for dice rolling anim, int for max round count till draw
            Game game = new Game(false, false, 10);
            game.GameInit();
        }
    }
}