using cytos.Game.Graphics.Object;
using cytos.Game.Tests.Visual;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

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
