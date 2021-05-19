using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneLoadingSpinner : CytosTestScene
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
    }
}
