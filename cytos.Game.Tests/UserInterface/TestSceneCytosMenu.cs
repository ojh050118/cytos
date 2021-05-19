using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using NUnit.Framework;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneCytosMenu : cytosTestScene
    {
        private CytosMenu menu;
        private bool actionPerformed;

        [SetUp]
        public void Setup() => Schedule(() =>
        {
            actionPerformed = false;

            Child = menu = new CytosMenu(Direction.Vertical, true)
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

        private void performAction() => actionPerformed = true;
    }
}
