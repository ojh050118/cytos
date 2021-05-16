using osu.Framework.Allocation;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Extensions.Color4Extensions;
using osuTK.Graphics;

namespace cytos.Game.Graphics.UserInterface
{
    public class CytosTextBox : BasicTextBox
    {
        public CytosTextBox()
        {
            Height = 40;
            CornerRadius = 10;
            LengthLimit = 100;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            BackgroundUnfocused = BackgroundFocused = Color4.Black.Opacity(0.5f);
            BackgroundCommit = new Color4(30, 30, 30, 255);
            BorderColour = Color4.SkyBlue;
            Placeholder.Colour = Color4.SkyBlue;
        }

        protected override void OnFocus(FocusEvent e)
        {
            BorderThickness = 3;
            base.OnFocus(e);
        }

        protected override void OnFocusLost(FocusLostEvent e)
        {
            if (!IsHovered)
                BorderThickness = 0;

            base.OnFocusLost(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            BorderThickness = 3;
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!HasFocus)
                BorderThickness = 0;

            base.OnHoverLost(e);
        }
    }
}
