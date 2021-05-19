using cytos.Game;
using osu.Framework;
using osu.Framework.Platform;

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
