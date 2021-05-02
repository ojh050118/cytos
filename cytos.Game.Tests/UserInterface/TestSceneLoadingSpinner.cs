using osu.Framework.Graphics;

using cytos.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics.Containers;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneLoadingSpinner : cytosTestScene
    {
        private LoadingSpinner loading;

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(loading = new LoadingSpinner(true, true)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
            AddStep("Show", () => loading.Show());
            AddStep("Hide", () => loading.Hide());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }
    }
}
