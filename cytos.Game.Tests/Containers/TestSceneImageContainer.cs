﻿using cytos.Game.Graphics.Containers;
using cytos.Game.Tests.Visual;
using osu.Framework.Graphics;
using osuTK;

namespace cytos.Game.Tests.Containers
{
    public class TestSceneImageContainer : CytosTestScene
    {
        public TestSceneImageContainer()
        {
            Add(new ImageContainer("menu-background")
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(100, 250)
            });
        }
    }
}
