using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cytos.Game.Graphics.Containers;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;
using osuTK;

namespace cytos.Game.Tests.Containers
{
    public class TestSceneImageContainer : cytosTestScene
    {
        private ImageContainer image;

        public TestSceneImageContainer()
        {
            Add(image = new ImageContainer("menu-background")
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(100, 250)
            });
        }
    }
}
