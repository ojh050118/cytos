using cytos.Game.Screens.Edit;
using cytos.Game.Tests.Visual;
using osu.Framework.Screens;

namespace cytos.Game.Tests.Screens
{
    public class TestSceneEditorScreen : CytosTestScene
    {
        public TestSceneEditorScreen()
        {
            Add(new ScreenStack(new EditorScreen()));
        }
    }
}
