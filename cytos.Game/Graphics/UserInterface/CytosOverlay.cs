using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK.Graphics;
using osuTK.Input;

namespace cytos.Game.Graphics.UserInterface
{
    public class CytosOverlay : OverlayContainer
    {
        protected override bool BlockPositionalInput => State.Value == Visibility.Visible;
        protected override bool StartHidden => true;

        protected virtual string MainText { get; set; }
        protected virtual string DescribeText { get; set; }
        private Container contents;

        private OverlayHeader top;
        private OverlayHeader bottom;

        public CytosOverlay()
        {
            //RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.Y;
            Width = 400;
            Child = new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Direction = FillDirection.Vertical,
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Children = new Drawable[]
                {
                    top = new OverlayHeader(),
                    contents = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.X,
                        Height = 500,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                Colour = new Color4(255, 255, 255, 30),
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.Both,
                            },
                            new Box
                            {
                                Colour = Color4.Black,
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                RelativeSizeAxes = Axes.X,
                                Height = 120
                            }
                        }
                    },
                    bottom = new OverlayHeader()
                }
            };
        }

        protected override void PopIn()
        {
            contents.ClearTransforms(true);
            top.ClearTransforms(true);
            bottom.ClearTransforms(true);
            contents.ResizeWidthTo(1, 500, Easing.OutQuint);
            if (!top.Expansion)
            {
                top.ToggleExpansion();
                bottom.ToggleExpansion();
            }

            top.FadeIn(300, Easing.OutQuint);
            bottom.FadeIn(300, Easing.OutQuint);
            Scheduler.AddDelayed(() =>
            {
                contents.ResizeHeightTo(500, 500, Easing.OutQuint);
            }, 300);
        }

        protected override void PopOut()
        {
            contents.ClearTransforms(true);
            top.ClearTransforms(true);
            bottom.ClearTransforms(true);
            contents.ResizeHeightTo(0, 500, Easing.OutQuint);
            Scheduler.AddDelayed(() =>
            {
                contents.ResizeWidthTo(0, 500, Easing.OutQuint);
                if (top.Expansion)
                {
                    top.ToggleExpansion();
                    bottom.ToggleExpansion();
                }

                Scheduler.AddDelayed(() =>
                {
                    top.FadeOut(300, Easing.OutQuint);
                    bottom.FadeOut(300, Easing.OutQuint);
                }, 300);
            }, 300);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (!e.Repeat)
            {
                switch (e.Key)
                {
                    case Key.Escape:
                        Hide();
                        return true;
                }
            }

            return base.OnKeyDown(e);
        }
    }
}
