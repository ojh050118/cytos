using cytos.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;

namespace cytos.Game.Screens
{
    public class MainScreen : CytosScreen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new CytosTextFlowContainer(s => s.Font = FontUsage.Default.With(size: 80))
                {
                    Text = "C Y T O S",
                    Direction = FillDirection.Horizontal,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding { Bottom = 100 },
                    AutoSizeAxes = Axes.Both
                }.WithEffect(new GlowEffect
                {
                    BlurSigma = new Vector2(3f),
                    Strength = 1.5f,
                    Colour = Colour4.Aqua,
                    PadExtent = true,
                }),
                new SpriteText
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding { Bottom = 30 },
                    Text = "TOUCH SCREEN TO START"
                }
            };
        }

        protected override void OnExit()
        {
        }

        protected override bool OnClick(ClickEvent e)
        {
            this.Push(new SelectScreen());

            return base.OnClick(e);
        }
    }
}
