using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace cytos.Game.Tests.Visual
{
    public class TestScenecytosGame : CytosTestScene
    {
        private CytosGame game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new CytosGame();
            game.SetHost(host);

            Add(game);
        }
    }
}
