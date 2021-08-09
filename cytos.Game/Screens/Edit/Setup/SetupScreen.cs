using System;
using cytos.Game.Beatmap;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.IO;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens.Edit.Setup
{
    public class SetupScreen : EditModeScreen
    {
        private BeatmapInfo info;

        private readonly bool isLoadedInfo = false;

        private Sprite background;

        public SetupScreen()
            : base(EditMode.Setup)
        {
        }

        public SetupScreen(BeatmapInfo info)
            : base(EditMode.Setup)
        {
            this.info = info;
            isLoadedInfo = true;
        }

        /// <summary>
        /// background, track, artist, title, start BPM, Offset
        /// </summary>
        public static LabelledTextBox[] TextBoxes = new LabelledTextBox[6];

        [BackgroundDependencyLoader]
        private void load(BackgroundImageStore imageStore)
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
                                    new Container
                                    {
                                        RelativeSizeAxes = Axes.X,
                                        Height = 150,
                                        Masking = true,
                                        CornerRadius = 10,
                                        Child = background = new Sprite
                                        {
                                        }
                                    },
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
                                    TextBoxes[4] = new LabelledTextBox("Start BPM"),
                                    TextBoxes[5] = new LabelledTextBox("Offset"),
                                    new Container
                                    {
                                        RelativeSizeAxes = Axes.X,
                                        Height = 150
                                    }
                                }
                            }
                        },
                    }
                }
            };

            try
            {
                TextBoxes[0].Current.ValueChanged += value => background.Texture = imageStore.Get(value.NewValue);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Background does not exist: {TextBoxes[0].Current.Value}");
            }

            if (isLoadedInfo)
            {
                TextBoxes[0].TextBox.Current.Value = info.Background;
                TextBoxes[1].TextBox.Current.Value = info.Track;
                TextBoxes[2].TextBox.Current.Value = info.Author;
                TextBoxes[3].TextBox.Current.Value = info.Title;
                TextBoxes[4].TextBox.Current.Value = info.BPM.ToString();
                TextBoxes[5].TextBox.Current.Value = info.Offset.ToString();
            }
        }
    }
}
