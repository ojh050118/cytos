using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneBoxButton : CytosTestScene
    {
        public TestSceneBoxButton()
        {
            Add(new BoxButton(Hide)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = "Click me",
            });
        }
    }
}
