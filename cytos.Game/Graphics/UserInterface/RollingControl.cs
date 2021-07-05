using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Graphics.UserInterface
{
    public class RollingControl : Container
    {
        public string Text {  get; set; }

        public RollingControl()
        {
            CornerRadius = 5;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.DarkGray,
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.3f
                },
                new SpriteText
                {
                    Text = Text,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding { Left = 20 }
                },
                new Container
                {
                    Margin = new MarginPadding { Right = 20 },
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                    RelativeSizeAxes = Axes.X,
                    Height = 50,
                    Width = 0.3f,
                    Children = new Drawable[]
                    {
                        new SpriteIcon
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Icon = FontAwesome.Solid.AngleLeft,
                            Size = new Vector2(25)
                        },
                        new SpriteIcon
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Icon = FontAwesome.Solid.AngleRight,
                            Size = new Vector2(25)
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = Text
                        }
                    }
                }
            };
        }
    }
}
