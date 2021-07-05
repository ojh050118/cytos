using cytos.Game.Graphics.Object;
using cytos.Game.Tests.Visual;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Tests.Objects
{
    public class TestSceneClickNote : CytosTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new ClickNote
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
        }
    }
}
