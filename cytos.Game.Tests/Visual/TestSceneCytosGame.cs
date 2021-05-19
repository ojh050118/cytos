using System;
using System.Collections.Generic;
using cytos.Game.Configuration;
using cytos.Game.Graphics.Backgrounds;
using cytos.Game.IO;
using cytos.Game.Overlays;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osuTK.Graphics;

namespace cytos.Game.Tests.Visual
{
    public class TestSceneCytosGame : CytosTestScene
    {
        private IReadOnlyList<Type> requiredGameDependencies => new[]
        {
            typeof(CytosGame),
            typeof(Background),
            typeof(DialogOverlay)
        };

        private IReadOnlyList<Type> requiredGameBaseDependencies => new[]
        {
            typeof(CytosConfigManager),
            typeof(BeatmapAudioManager),
            typeof(LargeTextureStore),
            typeof(BackgroundImageStore)
        };

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            CytosGame game = new CytosGame();
            game.SetHost(host);

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black
                },
                game
            };

            AddUntilStep("wait for load", () => game.IsLoaded);

            AddAssert("Check CytosGame DI members", () =>
            {
                foreach (var type in requiredGameDependencies)
                {
                    if (game.Dependencies.Get(type) == null)
                        throw new InvalidOperationException($"{type} has not been cached");
                }

                return true;
            });
            AddAssert("Check CytosGameBase DI members", () =>
            {
                foreach (var type in requiredGameBaseDependencies)
                {
                    if (game.Dependencies.Get(type) == null)
                        throw new InvalidOperationException($"{type} has not been cached");
                }

                return true;
            });
        }
    }
}
