using osu.Framework.Platform;
using osu.Framework;
using cytos.Game;

namespace cytos.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"cytos"))
            using (osu.Framework.Game game = new cytosGame())
                host.Run(game);
        }
    }
}
