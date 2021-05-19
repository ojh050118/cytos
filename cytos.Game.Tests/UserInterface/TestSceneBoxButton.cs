using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneBoxButton : cytosTestScene
    {
        public TestSceneBoxButton()
        {
            Add(new BoxButton(() => Hide())
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = "Click me",
                //RelativeSizeAxes = Axes.X,
                //Padding = new MarginPadding { Horizontal = 100 }
            });
        }
    }
}
