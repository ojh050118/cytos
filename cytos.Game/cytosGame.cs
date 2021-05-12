using cytos.Game.Graphics.Backgrounds;
using cytos.Game.Overlays;
using cytos.Game.Screens;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;

namespace cytos.Game
{
    public class cytosGame : cytosGameBase
    {
        private ScreenStack screenStack;
        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            //Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };

            ScreenStack screens = new ScreenStack();
            var background = new Background("Backgrounds/bg1");
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
                    }
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            //screenStack.Push(new MainScreen());
        }
    }
}
