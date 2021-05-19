using System;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.IO;
using cytos.Game.Overlays;
using cytos.Game.Screens.Edit.Setup;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens.Edit
{
    public class EditorScreen : CytosScreen
    {
        private Container screenContainer;
        private readonly Action addScreen;
        private CytosTabControl<EditMode> tabControl;
        private CytosTabControl<Speed> playback;
        private DrawableTrack track;
        private CircleButton play;
        private string currentTrack = string.Empty;

        private BeatmapAudioManager audioManager;

        [Resolved]
        private DialogOverlay dialog { get; set; }

        public EditorScreen(Drawable drawable = null)
        {
            if (drawable is not null)
                addScreen = () => screenContainer.Add(drawable);
            else
            {
                addScreen = () => screenContainer.Add(new ScreenStack(new SetupScreen()));
            }
        }

        [BackgroundDependencyLoader]
        private void load(BeatmapAudioManager beatmapAudio)
        {
            audioManager = beatmapAudio;

            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 40,
                    Name = "Menu + Sections",
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black
                        },
                        new Container
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            RelativeSizeAxes = Axes.Y,
                            AutoSizeAxes = Axes.X,
                            Padding = new MarginPadding { Bottom = 5, Left = 10 },
                            Child = tabControl = new CytosTabControl<EditMode>
                            {
                                Anchor = Anchor.CentreRight,
                                Origin = Anchor.CentreRight,
                                RelativeSizeAxes = Axes.Y,
                                Width = 200,
                                AutoSort = true
                            }
                        }
                    }
                },
                new Container
                {
                    Name = "Time control tab",
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 80,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(20, 20, 20, 255)
                        },
                        new Container
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            Padding = new MarginPadding(10),
                            RelativeSizeAxes = Axes.Y,
                            AutoSizeAxes = Axes.X,
                            Child = new Container
                            {
                                RelativeSizeAxes = Axes.Y,
                                AutoSizeAxes = Axes.X,
                                Masking = true,
                                CornerRadius = 10,
                                Children = new Drawable[]
                                {
                                    new Box
                                    {
                                        RelativeSizeAxes = Axes.Y,
                                        Width = 300,
                                        Colour = new Color4(40, 40, 40, 255)
                                    },
                                    play = new CircleButton
                                    {
                                        Anchor = Anchor.CentreLeft,
                                        Origin = Anchor.CentreLeft,
                                        Margin = new MarginPadding { Left = 10 },
                                        Icon = FontAwesome.Solid.Play,
                                        Size = new Vector2(40)
                                    },
                                    new SpriteText
                                    {
                                        Padding = new MarginPadding{ Top = 10, Left = 60 },
                                        Text = "Playback speed"
                                    },
                                    playback = new CytosTabControl<Speed>
                                    {
                                        Anchor = Anchor.BottomLeft,
                                        Origin = Anchor.BottomLeft,
                                        Margin = new MarginPadding{ Bottom = 10, Left = 60 },
                                        AutoSort = true,
                                        Size = new Vector2(240, 20),
                                    }
                                }
                            }
                        }
                    }
                },
                screenContainer = new Container
                {
                    Padding = new MarginPadding { Top = 40, Bottom = 80 },
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Scale = new Vector2(0.8f),
                    Name = "Screen Container"
                },
                track = new DrawableTrack(new TrackVirtual(1000)),
            };

            play.Action += () =>
            {
                try
                {
                    if (!track.IsRunning)
                    {
                        if (!currentTrack.Equals(SetupScreen.TextBoxes[1].Current.Value))
                        {
                            track = new DrawableTrack(audioManager.Tracks.Get(SetupScreen.TextBoxes[1].Current.Value));
                            currentTrack = SetupScreen.TextBoxes[1].Current.Value;
                        }

                        track.Start();
                        play.Icon = FontAwesome.Solid.Pause;
                    }
                    else
                    {
                        track.Stop();
                        play.Icon = FontAwesome.Solid.Play;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e, $"Track does not exist: {SetupScreen.TextBoxes[1].Current.Value}");
                    track.Stop();
                    play.Icon = FontAwesome.Solid.Play;
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            tabControl.PinItem(EditMode.Setup);
            tabControl.PinItem(EditMode.Compose);
            tabControl.PinItem(EditMode.Timing);
            screenContainer.ScaleTo(1, 300, Easing.OutQuint);
            addScreen.Invoke();
            playback.Current.Value = Speed.Normal;
        }

        protected override void OnExit()
        {
            dialog.Push("Are you sure you want to exit? All unsaved progress will be lost.", () =>
            {
                track.Stop();
                play.Icon = FontAwesome.Solid.Play;
                base.OnExit();
            });
        }
    }

    public enum EditMode
    {
        Setup,
        Compose,
        Timing
    }

    public enum Speed
    {
        Veryslow = 25,
        Slow = 50,
        Littleslow = 75,
        Normal = 100
    }
}
