using cytos.Game.Screens.Play;
using cytos.Game.Tests.Visual;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Tests.Play
{
    public class TestScenePlayfield : CytosTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new ScreenStack(new Playfield()) { RelativeSizeAxes = Axes.Both });
        }
    }
}
