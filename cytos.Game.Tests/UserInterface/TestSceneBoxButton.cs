using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;

namespace cytos.Game.Tests.UserInterface
{
    public class TestSceneBoxButton : cytosTestScene
    {
        public TestSceneBoxButton()
        {
            Add(new BoxButton(() => Show())
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Text = "Click me"
            });
            Add(new BoxButton(() => Show(), true)
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                Text = "muyaho"
            });
        }
    }
}
