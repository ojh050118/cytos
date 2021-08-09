using cytos.Game.Beatmap;
using cytos.Game.Graphics.Backgrounds;
using cytos.Game.IO;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens.Play
{
    public class Playfield : CytosScreen
    {
        private Background bg;
        private Box top;
        private Box bottom;
        private Box judgeLine;
        private BeatmapInfo info;
        private bool onEditor;
        private DrawableTrack track;
        private Track trackSource;

        public Playfield(BeatmapInfo info, bool onEditor = false)
        {
            this.info = info;
            this.onEditor = onEditor;
        }

        [BackgroundDependencyLoader]
        private void load(BeatmapAudioManager audio)
        {
            if (!info.Track.Equals(string.Empty))
                trackSource = audio.Tracks.Get(info.Track);

            if (info.Background is not null)
            {
                bg = new Background(info.Background.Equals(string.Empty) ? @"Backgrounds/bg1" : info.Background, !info.Background.Equals(string.Empty))
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                };
            }
            else
            {
                bg = new Background("Backgrounds/bg1", false)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                };

            }

            InternalChildren = new Drawable[]
            {
                bg,
                new Container
                {
                    Padding = new MarginPadding { Vertical = 50 },
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Name = "Playfield",
                    Children = new Drawable[]
                    {
                        top = new Box
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Height = 3,
                            RelativeSizeAxes = Axes.X,
                            Width = 0,
                            Colour = Color4.White
                        },
                        bottom = new Box
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            Height = 3,
                            RelativeSizeAxes = Axes.X,
                            Width = 0,
                            Colour = Color4.White
                        },
                        judgeLine = new Box
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Height = 3,
                            RelativeSizeAxes = Axes.X,
                            Width = 0,
                            Colour = Color4.White
                        }

                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            top.ResizeWidthTo(1, 1000, Easing.OutPow10);
            bottom.ResizeWidthTo(1, 1000, Easing.OutPow10);
            judgeLine.ResizeWidthTo(1, 1000, Easing.OutPow10);
            Scheduler.AddDelayed(() => bg.BlurTo(new Vector2(10), 1000, Easing.OutPow10), 150);

            if (!onEditor)
            {
                if (info.BPM != 0)
                    Scheduler.AddDelayed(() =>
                    {
                        judgeLine.MoveToY(DrawHeight - 100, 60000 / info.BPM).Then().MoveToY(0, 60000 / info.BPM).Loop();
                        top.ResizeHeightTo(9, 0, Easing.OutQuint).Then().ResizeHeightTo(3, 60000 / info.BPM, Easing.OutQuint).Then().Loop();
                        bottom.ResizeHeightTo(9, 0, Easing.OutQuint).Then().ResizeHeightTo(3, 60000 / info.BPM, Easing.OutQuint).Then().Loop();
                    }, info.Offset);

                if (trackSource is not null)
                {
                    track = new DrawableTrack(trackSource);
                    track.Start();
                }
            }
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (track is not null)
            {
                track.Stop();
                track.Dispose();
            }
        }
    }
}
