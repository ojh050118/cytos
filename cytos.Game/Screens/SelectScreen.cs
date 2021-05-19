using cytos.Game.Graphics.UserInterface;
using cytos.Game.Screens.Edit;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens
{
    public class SelectScreen : CytosScreen
    {
        private FillFlowContainer beatmaps;
        private const float button_size = 50;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                // Todo: 나중에 사용함.
                new Box
                {
                    Colour = new Color4(30, 30, 30, 255),
                    RelativeSizeAxes = Axes.Both,
                },
                new BasicScrollContainer(Direction.Horizontal)
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Left = 25, Right = 25, Top = 50, Bottom = 70 },
                    Child = beatmaps = new FillFlowContainer
                    {
                        Direction = FillDirection.Horizontal,
                        RelativeSizeAxes = Axes.Y,
                        AutoSizeAxes = Axes.X,
                        Spacing = new Vector2(10),
                        Children = new Drawable[]
                        {
                        }
                    }
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Height = button_size,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding { Top = 10, Bottom = 10 },
                    Padding = new MarginPadding { Left = 10, Right = 10 },
                    Children = new Drawable[]
                    {
                        new FillFlowContainer<CircleButton>
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Children = new[]
                            {
                                new CircleButton(this.Exit)
                                {
                                    Icon = FontAwesome.Solid.ArrowLeft,
                                    Size = new Vector2(button_size),
                                },
                            }
                        },
                        new FillFlowContainer<CircleButton>
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Children = new[]
                            {
                                new CircleButton
                                {
                                    Icon = FontAwesome.Solid.Cog,
                                    Size = new Vector2(button_size),
                                }
                            }
                        },
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            beatmaps.Add(new ClickableContainer
            {
                Masking = true,
                CornerRadius = 10,
                RelativeSizeAxes = Axes.Y,
                Width = 100,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = new Color4(80, 80, 80, 200),
                    },
                    new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Icon = FontAwesome.Solid.Plus,
                        Size = new Vector2(30)
                    }
                },
                Action = () => this.Push(new EditorScreen())
            });
        }
    }
}
