using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Graphics.Object
{
    public class ClickNote : CircularContainer
    {
        public const float NOTE_SIZE = 100;

        private Circle inner;

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            Size = new Vector2(NOTE_SIZE);
            Alpha = 0;
            Scale = new Vector2(0.5f);
            Children = new Drawable[]
            {
                new Circle
                {
                    Colour = Color4.White,
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                inner = new Circle
                {
                    Colour = Color4.DeepSkyBlue,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.4f),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            this.ScaleTo(1, 1000, Easing.OutPow10);
            this.FadeIn(800, Easing.OutPow10);
            inner.ResizeTo(0.8f, 1200, Easing.OutPow10);
        }

        protected override bool OnClick(ClickEvent e)
        {
            this.ScaleTo(1.2f, 500, Easing.OutPow10);
            this.FadeOut(400, Easing.OutPow10);

            return base.OnClick(e);
        }
    }
}
