using cytos.Game.Screens.Edit;
using cytos.Game.Tests.Visual;
using osu.Framework.Screens;

namespace cytos.Game.Tests.Screens
{
    public class TestSceneEditorScreen : cytosTestScene
    {
        public TestSceneEditorScreen()
        {
            Add(new ScreenStack(new EditorScreen()));
        }
    }
}
