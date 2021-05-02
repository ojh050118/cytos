using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;

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
