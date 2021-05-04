using cytos.Game.Graphics.Backgrounds;
using cytos.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;

namespace cytos.Game
{
    public class MainScreen : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Background("Backgrounds/bg1")
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new CytosTextFlowContainer(s => s.Font = FontUsage.Default.With(size: 40))
                {
                    Text = "C Y T O S",
                    Direction = FillDirection.Horizontal,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding { Bottom = 100 },
                    AutoSizeAxes = Axes.Both
                },
                new SpriteText
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Margin = new MarginPadding { Bottom = 30 },
                    Text = "TOUCH SCREEN TO START"
                }
            };
        }
    }
}
