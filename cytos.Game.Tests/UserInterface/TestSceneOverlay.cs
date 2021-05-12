using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneOverlay : cytosTestScene
    {
        private CytosOverlay overlay;

        public TestSceneOverlay()
        {
            Add(overlay = new CytosOverlay
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
            AddStep("", () => Show());
            AddStep("Show", () => overlay.Show());
            AddStep("Hide", () => overlay.Hide());
        }
    }
}
