using cytos.Game.Screens;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace cytos.Game.Tests.Visual
{
    public class TestSceneMainScreen : CytosTestScene
    {
        public TestSceneMainScreen()
        {
            Add(new ScreenStack(new MainScreen()) { RelativeSizeAxes = Axes.Both });
        }
    }
}
