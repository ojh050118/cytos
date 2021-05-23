using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace cytos.Game.Screens.Edit
{
    public class EditModeScreen : Container
    {
        protected override Container<Drawable> Content => content;
        private readonly Container content;

        public readonly EditMode Type;

        protected EditModeScreen(EditMode type)
        {
            Type = type;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;

            InternalChild = content = new Container { RelativeSizeAxes = Axes.Both };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            this.FadeTo(0)
                .Then()
                .FadeTo(1f, 250, Easing.OutQuint);
        }
    }
}
