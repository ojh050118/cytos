using System.ComponentModel;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.Screens.Edit;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneTabControl : cytosTestScene
    {
        public TestSceneTabControl()
        {
            SpriteText text;

            CytosTabControl<EditMode> tabControl;

            Add(tabControl = new CytosTabControl<EditMode>
            {
                Margin = new MarginPadding(4),
                Size = new Vector2(229, 24),
                AutoSort = true,
                SelectFirstTabByDefault = true
            });
            Add(text = new SpriteText
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            });

            tabControl.Current.ValueChanged += grouping =>
            {
                text.Text = grouping.NewValue.ToString();
            };
            tabControl.PinItem(EditMode.Setup);
        }
    }
}
