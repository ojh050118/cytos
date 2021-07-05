using cytos.Game.Graphics.Backgrounds;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens.Play
{
    public class Playfield : CytosScreen
    {
        private Background bg;
        private Box top;
        private Box bottom;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                bg = new Background(@"Backgrounds/bg1")
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
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

                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            top.ResizeWidthTo(1, 1000, Easing.OutPow10);
            bottom.ResizeWidthTo(1, 1000, Easing.OutPow10);
            Scheduler.AddDelayed(() => bg.BlurTo(new Vector2(10), 1000, Easing.OutPow10), 150);
        }
    }
}
