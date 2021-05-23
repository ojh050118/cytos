using cytos.Game.Graphics.UserInterface;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;

namespace cytos.Game.Screens.Edit.Components.Menus
{
    public class ScreenSelectionTabControl : CytosTabControl<EditMode>
    {
        public ScreenSelectionTabControl()
        {
            AutoSizeAxes = Axes.X;
            RelativeSizeAxes = Axes.Y;

            TabContainer.RelativeSizeAxes &= ~Axes.X;
            TabContainer.AutoSizeAxes = Axes.X;
            TabContainer.Padding = new MarginPadding();

            AddInternal(new Box
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                RelativeSizeAxes = Axes.X,
                Height = 1,
                Colour = Color4.White.Opacity(0.2f),
            });
        }
    }
}
