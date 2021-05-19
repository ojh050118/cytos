using cytos.Game.Graphics.Backgrounds;
using cytos.Game.Overlays;
using cytos.Game.Screens;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;

namespace cytos.Game
{
    public class CytosGame : CytosGameBase
    {
        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            ScreenStack screens = new ScreenStack();

            var background = new Background("Backgrounds/bg1");
            dependencies.Cache(background);

            DialogOverlay dialog;
            dependencies.Cache(dialog = new DialogOverlay());

            screens.Push(new MainScreen());

            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Name = "MainScreen",
                Child = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        background,
                        screens,
                        dialog
                    }
                }
            });
        }
    }
}
