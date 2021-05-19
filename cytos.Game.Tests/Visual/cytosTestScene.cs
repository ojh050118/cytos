using osu.Framework.Testing;

namespace cytos.Game.Tests.Visual
{
    public class CytosTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new CytosTestSceneTestRunner();

        private class CytosTestSceneTestRunner : CytosGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
