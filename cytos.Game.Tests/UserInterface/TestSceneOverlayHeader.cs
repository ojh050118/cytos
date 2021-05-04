using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneOverlayHeader : cytosTestScene
    {
        private OverlayHeader header;
        public TestSceneOverlayHeader()
        {
            Add(header = new OverlayHeader()
            {
            });
            AddStep("Toggle", () => header.ToggleExpansion());
        }
    }
}
