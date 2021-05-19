using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using NUnit.Framework;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneCytosMenu : CytosTestScene
    {
        [SetUp]
        public void Setup() => Schedule(() =>
        {

            Child = new CytosMenu(Direction.Vertical, true)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Items = new[]
                {
                    new CytosMenuItem("standard", MenuItemType.Standard, performAction),
                    new CytosMenuItem("highlighted", MenuItemType.Highlighted, performAction),
                    new CytosMenuItem("destructive", MenuItemType.Destructive, performAction),
                }
            };
        });

        private void performAction() => Show();
    }
}
