using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Transforms;
using osuTK;

namespace cytos.Game.Graphics.Backgrounds
{
    public class Background : CompositeDrawable
    {
        private const float blur_scale = 0.5f;

        public readonly Sprite Sprite;

        private readonly string textureName;

        private BufferedContainer bufferedContainer;

        public Background(string textureName = @"")
        {
            this.textureName = textureName;
            RelativeSizeAxes = Axes.Both;

            AddInternal(Sprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                FillMode = FillMode.Fill,
            });
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures)
        {
            if (!string.IsNullOrEmpty(textureName))
                Sprite.Texture = textures.Get(textureName);
        }

        public Vector2 BlurSigma => bufferedContainer?.BlurSigma / blur_scale ?? Vector2.Zero;

        public void BlurTo(Vector2 newBlurSigma, double duration = 0, Easing easing = Easing.None)
        {
            if (bufferedContainer == null && newBlurSigma != Vector2.Zero)
            {
                RemoveInternal(Sprite);

                AddInternal(bufferedContainer = new BufferedContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CacheDrawnFrameBuffer = true,
                    RedrawOnScale = false,
                    Child = Sprite
                });
            }

            if (bufferedContainer != null)
                bufferedContainer.FrameBufferScale = newBlurSigma == Vector2.Zero ? Vector2.One : new Vector2(blur_scale);

            bufferedContainer?.BlurTo(newBlurSigma * blur_scale, duration, easing);
        }
    }
}
