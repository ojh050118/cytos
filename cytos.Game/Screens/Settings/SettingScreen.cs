using cytos.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens.Settings
{
    public class SettingScreen : CytosScreen
    {
        private Box bg;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                bg = new Box
                {
                    Colour = new Color4(30, 30, 30, 255),
                    RelativeSizeAxes = Axes.Both,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            AutoSizeAxes = Axes.Both,
                            Margin = new MarginPadding { Top = 30, Left = 30 },
                            Masking = true,
                            CornerRadius = 5,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Colour = Color4.Black,
                                    Alpha = 0.5f,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    //RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(93, 30),
                                    Scale = new Vector2(1.2f)
                                },
                                new SpriteText
                                {
                                    Text = "settings",
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Font = FontUsage.Default.With(size: 30)
                                }
                            }
                        },
                        new Container
                        {
                            Width = 600,
                            Padding = new MarginPadding { Top = 90, Left = 30, Bottom = 30 },
                            RelativeSizeAxes = Axes.Y,
                            Child = new Container
                            {
                                Masking = true,
                                CornerRadius = 5,
                                RelativeSizeAxes = Axes.Both,
                                Children = new Drawable[]
                                {
                                    new Box
                                    {
                                        Colour = Color4.Black,
                                        Alpha = 0.5f,
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        RelativeSizeAxes = Axes.Both,
                                    },
                                    new BasicScrollContainer
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Child = new FillFlowContainer
                                        {
                                            Direction = FillDirection.Vertical,
                                            Spacing = new Vector2(5),
                                            AutoSizeAxes = Axes.Y,
                                            RelativeSizeAxes = Axes.X,
                                            Children = new Drawable[]
                                            {
                                                new RollingControl
                                                {
                                                    Text = "volume"
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                        }
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Scheduler.AddDelayed(() => bg.FadeTo(0.7f, 2000, Easing.OutPow10), 270);
        }
    }
}
