using cytos.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens.Edit.Setup
{
    public class SetupScreen : EditModeScreen
    {
        public SetupScreen()
            : base(EditMode.Setup)
        {
        }

        /// <summary>
        /// background, track, artist, title, start BPM
        /// </summary>
        public static LabelledTextBox[] TextBoxes = new LabelledTextBox[5];

        [BackgroundDependencyLoader]
        private void load()
        {
            Padding = new MarginPadding(50);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Masking = true,
                    CornerRadius = 15,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(30, 30, 40, 255)
                        },
                        new Container
                        {
                            Name = "Header",
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Colour = Color4.Black.Opacity(0.5f),
                                    RelativeSizeAxes = Axes.Both
                                },
                                new FillFlowContainer
                                {
                                    Name = "Header",
                                    Padding = new MarginPadding(20),
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Direction = FillDirection.Horizontal,
                                    Spacing = new Vector2(20),
                                    Children = new Drawable[]
                                    {
                                        new SpriteIcon
                                        {
                                            Icon = FontAwesome.Solid.Tools,
                                            Size = new Vector2(30)
                                        },
                                        new SpriteText
                                        {
                                            Text = "beatmap setup",
                                            Font = FontUsage.Default.With(size: 30)
                                        }
                                    }
                                }
                            }
                        },
                        new BasicScrollContainer
                        {
                            Margin = new MarginPadding { Top = 70 },
                            RelativeSizeAxes = Axes.Both,
                            Child = new FillFlowContainer
                            {
                                Direction = FillDirection.Vertical,
                                Spacing = new Vector2(15),
                                Padding = new MarginPadding { Top = 20, Left = 30, Right = 30 },
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Children = new Drawable[]
                                {
                                    new SpriteText
                                    {
                                        Text = "Resources"
                                    },
                                    TextBoxes[0] = new LabelledTextBox("Background"),
                                    TextBoxes[1] = new LabelledTextBox("Audio Track"),
                                    new SpriteText
                                    {
                                        Text = "Metadata"
                                    },
                                    TextBoxes[2] = new LabelledTextBox("Artist"),
                                    TextBoxes[3] = new LabelledTextBox("Title"),
                                    TextBoxes[4] = new LabelledTextBox("Start BPM")
                                }
                            }
                        },
                    }
                }
            };
        }
    }
}
