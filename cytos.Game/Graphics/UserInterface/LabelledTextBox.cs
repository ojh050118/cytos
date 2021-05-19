using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK.Graphics;

namespace cytos.Game.Graphics.UserInterface
{
    public class LabelledTextBox : Container
    {
        private const float corner_radius = 20;

        private string text { get; init; }

        private CytosTextBox textBox;

        public Bindable<string> Current = new Bindable<string>("");

        public LabelledTextBox(string text)
        {
            this.text = text;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            CornerRadius = corner_radius;
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.DarkGray.Opacity(0.3f),
                    RelativeSizeAxes = Axes.Both,
                },
                new SpriteText
                {
                    Text = text,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding { Left = 20 }
                },
                textBox = new CytosTextBox
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.X,
                    Width = 0.8f,
                    Height = 50,
                    CornerRadius = corner_radius,
                }
            };
            textBox.Current.ValueChanged += value =>
            {
                Current.Value = value.NewValue;
            };
        }
    }
}
