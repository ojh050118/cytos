using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneBoxButton : CytosTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new BoxButton(null)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = "Click me",
            });
        }
    }
}
