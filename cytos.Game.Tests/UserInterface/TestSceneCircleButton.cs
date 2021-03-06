using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneCircleButton : cytosTestScene
    {
        private CircleButton button;

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(button = new CircleButton
            {
                Icon = FontAwesome.Solid.List,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(100)
            });
        }
    }
}
