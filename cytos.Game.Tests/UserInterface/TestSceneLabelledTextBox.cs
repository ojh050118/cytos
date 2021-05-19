using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneLabelledTextBox : CytosTestScene
    {
        public TestSceneLabelledTextBox()
        {
            Add(new LabelledTextBox("test")
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}
