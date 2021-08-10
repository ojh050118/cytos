using System;
using System.Collections.Generic;
using System.Linq;
using cytos.Game.Beatmap;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.IO;
using cytos.Game.Overlays;
using cytos.Game.Screens.Edit.Components.Menus;
using cytos.Game.Screens.Edit.Compose;
using cytos.Game.Screens.Edit.Setup;
using cytos.Game.Screens.Edit.Timing;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens.Edit
{
    public class EditorScreen : CytosScreen
    {
        private Container<EditModeScreen> screenContainer;
        private readonly Action addScreen;
        private CytosTabControl<Speed> playback;
        private DrawableTrack track;
        private CircleButton play;
        private string currentTrack = string.Empty;
        private EditModeScreen currentScreen;
        private EditorMenuBar menuBar;
        private SpriteText time;

        private BeatmapAudioManager audioManager;

        private BeatmapStorage beatmap;

        [Resolved]
        private DialogOverlay dialog { get; set; }

        public static BeatmapInfo CurrentInfo = new BeatmapInfo();
        private readonly bool isLoadedInfo = false;

        public static Bindable<double> CurrentTime = new Bindable<double>(0);

        public EditorScreen(EditModeScreen drawable = null)
        {
            if (drawable is not null)
                addScreen = () => screenContainer.Add(drawable);
            else
                addScreen = () => screenContainer.Add(currentScreen = new SetupScreen());

            CurrentInfo = new BeatmapInfo();
        }

        public EditorScreen(BeatmapInfo info)
        {
            isLoadedInfo = true;
            CurrentInfo = info;
            addScreen = () => screenContainer.Add(currentScreen = new SetupScreen(info));

        }

        #region Load()

        [BackgroundDependencyLoader]
        private void load(BeatmapAudioManager beatmapAudio, BeatmapStorage beatmaps)
        {
            audioManager = beatmapAudio;
            beatmap = beatmaps;
            EditorMenuItem save;
            if (CurrentInfo.Notes is null)
            {
                CurrentInfo.Notes = new List<Notes>();
                Logger.Log("List<Note> created.");
            }
            else
            {
                Logger.Log($"List<Notes> | Count: {CurrentInfo.Notes.Count}");
            }


            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 40,
                    Name = "top tab",
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 40,
                            Child = menuBar = new EditorMenuBar
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.Both,
                                Items = new[]
                                {
                                    new MenuItem("File")
                                    {
                                        Items = new[]
                                        {
                                            save = new EditorMenuItem("Save", MenuItemType.Standard,
                                            () => beatmap.CreateBeatmap(CurrentInfo.Title.Equals(string.Empty) ? "untitled" : CurrentInfo.Title, new BeatmapInfo
                                            {
                                                Author = CurrentInfo.Author,
                                                Background = CurrentInfo.Background,
                                                BPM = !SetupScreen.TextBoxes[4].Current.Value.Equals(string.Empty) ? CurrentInfo.BPM : 0,
                                                Title = CurrentInfo.Title.Equals(string.Empty) ? "untitled" : CurrentInfo.Title,
                                                Track = CurrentInfo.Track,
                                                Offset = !SetupScreen.TextBoxes[5].Current.Value.Equals(string.Empty) ? CurrentInfo.Offset : 0,
                                                Notes = CurrentInfo.Notes
                                            })),
                                            new EditorMenuItem("Exit", MenuItemType.Standard, OnExit)
                                        }
                                    },
                                    new MenuItem("Edit")
                                    {
                                        Items = new[]
                                        {
                                            new EditorMenuItem("Undo", MenuItemType.Standard),
                                            new EditorMenuItem("Redo", MenuItemType.Standard)
                                        }
                                    },
                                    new MenuItem("View")
                                    {

                                    }
                                }
                            }
                        }
                    }
                },
                new Container
                {
                    Name = "bottom tab",
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
                                        Padding = new MarginPadding { Top = 10, Left = 60 },
                                        Text = "Playback speed"
                                    },
                                    playback = new CytosTabControl<Speed>
                                    {
                                        Anchor = Anchor.BottomLeft,
                                        Origin = Anchor.BottomLeft,
                                        Margin = new MarginPadding { Bottom = 10, Left = 60 },
                                        AutoSort = true,
                                        Size = new Vector2(240, 20),
                                    }
                                }
                            }
                        },
                        time = new SpriteText
                        {
                            Padding = new MarginPadding(10),
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Font = FontUsage.Default.With(size: 39)
                        }
                    }
                },
                screenContainer = new Container<EditModeScreen>
                {
                    Padding = new MarginPadding { Top = 40, Bottom = 80 },
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Scale = new Vector2(0.8f),
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
                    Logger.Log($"Track does not exist: {CurrentInfo.Track}", level: LogLevel.Error);
                    track.Stop();
                    play.Icon = FontAwesome.Solid.Play;
                }
            };
        }

        #endregion

        #region LoadComplete()

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenContainer.ScaleTo(1, 300, Easing.OutQuint);
            addScreen.Invoke();
            playback.Current.Value = Speed.Normal;
            menuBar.Mode.ValueChanged += onModeChanged;
            time.Text = track.CurrentTime.ToString();
            CurrentTime.Value = track.CurrentTime;

            CurrentInfo.Background = SetupScreen.TextBoxes[0].Current.Value;
            CurrentInfo.Track = SetupScreen.TextBoxes[1].Current.Value;
            CurrentInfo.Author = SetupScreen.TextBoxes[2].Current.Value;
            CurrentInfo.Title = SetupScreen.TextBoxes[3].Current.Value;

            if (!SetupScreen.TextBoxes[4].Current.Value.Equals(string.Empty))
                CurrentInfo.BPM = float.Parse(SetupScreen.TextBoxes[4].Current.Value);
            if (!SetupScreen.TextBoxes[5].Current.Value.Equals(string.Empty))
                CurrentInfo.Offset = float.Parse(SetupScreen.TextBoxes[5].Current.Value);

            SetupScreen.TextBoxes[0].Current.ValueChanged += value => CurrentInfo.Background = value.NewValue;
            SetupScreen.TextBoxes[1].Current.ValueChanged += value => CurrentInfo.Track = value.NewValue;
            SetupScreen.TextBoxes[2].Current.ValueChanged += value => CurrentInfo.Author = value.NewValue;
            SetupScreen.TextBoxes[3].Current.ValueChanged += value => CurrentInfo.Title = value.NewValue;
            SetupScreen.TextBoxes[4].Current.ValueChanged += value => CurrentInfo.BPM = !SetupScreen.TextBoxes[4].Current.Value.Equals(string.Empty) ? float.Parse(value.NewValue) : 0;
            SetupScreen.TextBoxes[5].Current.ValueChanged += value => CurrentInfo.Offset = !SetupScreen.TextBoxes[5].Current.Value.Equals(string.Empty) ? float.Parse(value.NewValue) : 0;
        }

        #endregion

        #region Update()

        protected override void Update()
        {
            base.Update();

            time.Text = ((int)track.CurrentTime).ToString();
            CurrentTime.Value = track.CurrentTime;
        }

        #endregion

        #region onModeChanged()

        private void onModeChanged(ValueChangedEvent<EditMode> e)
        {
            var lastScreen = currentScreen;

            lastScreen?
                    .ScaleTo(0.9f, 200, Easing.OutQuint)
                    .FadeOut(200, Easing.OutQuint);

            try
            {
                // 바꾸려는 모드가 있는지 확인함. s: EditModeScreen.
                if ((currentScreen = screenContainer.SingleOrDefault(s => s.Type == e.NewValue)) != null)
                {
                    screenContainer.ChangeChildDepth(currentScreen, lastScreen?.Depth + 1 ?? 0);

                    currentScreen
                        .ScaleTo(1, 200, Easing.OutQuint)
                        .FadeIn(200, Easing.OutQuint);
                    return;
                }

                if (isLoadedInfo)
                {
                    switch (e.NewValue)
                    {
                        case EditMode.Setup:
                            currentScreen = new SetupScreen(CurrentInfo);
                            break;
                        case EditMode.Compose:
                            currentScreen = new ComposeScreen(CurrentInfo);
                            break;
                        case EditMode.Timing:
                            currentScreen = new TimingScreen();
                            break;
                    }
                }
                else
                {
                    switch (e.NewValue)
                    {
                        case EditMode.Setup:
                            currentScreen = new SetupScreen();
                            break;
                        case EditMode.Compose:
                            currentScreen = new ComposeScreen();
                            break;
                        case EditMode.Timing:
                            currentScreen = new TimingScreen();
                            break;
                    }
                }


                LoadComponentAsync(currentScreen, newScreen =>
                {
                    if (newScreen == currentScreen)
                        screenContainer.Add(newScreen);
                });
            }
            finally
            {
            }
        }

        #endregion

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
