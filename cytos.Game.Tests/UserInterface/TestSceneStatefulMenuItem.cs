using System;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneStatefulMenuItem : CytosTestScene
    {
        public TestSceneStatefulMenuItem()
        {
            AddStep("create menu", () =>
            {
                Child = new CytosMenu(Direction.Vertical, true)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Items = new[]
                    {
                        new TestMenuItem("first", MenuItemType.Standard, getNextState),
                        new TestMenuItem("second", MenuItemType.Standard, getNextState) { State = { Value = TestStates.State2 } },
                        new TestMenuItem("third", MenuItemType.Standard, getNextState) { State = { Value = TestStates.State3 } },
                    }
                };
            });
        }

        private TestStates getNextState(TestStates state)
        {
            switch (state)
            {
                case TestStates.State1:
                    return TestStates.State2;

                case TestStates.State2:
                    return TestStates.State3;

                case TestStates.State3:
                    return TestStates.State1;
            }

            return TestStates.State1;
        }

        private class TestMenuItem : StatefulMenuItem<TestStates>
        {
            public TestMenuItem(string text, MenuItemType type, Func<TestStates, TestStates> changeStateFunc)
                : base(text, changeStateFunc, type)
            {
            }

            public override IconUsage? GetIconForState(TestStates state)
            {
                switch (state)
                {
                    case TestStates.State1:
                        return FontAwesome.Solid.DiceOne;

                    case TestStates.State2:
                        return FontAwesome.Solid.DiceTwo;

                    case TestStates.State3:
                        return FontAwesome.Solid.DiceThree;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
            }
        }

        private enum TestStates
        {
            State1,
            State2,
            State3
        }
    }
}
