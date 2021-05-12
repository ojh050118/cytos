using System;
using System.Collections.Generic;
using System.Text;
using cytos.Game.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace cytos.Game.Graphics.Containers
{
    public class ImageContainer : ClickableContainer
    {
        public string Texture_Name { get; }

        public static int Radius = 10;

        private Action action;

        public ImageContainer(string name, Action action = null)
        {
            Texture_Name = name;
            this.action = action;
        }

        [BackgroundDependencyLoader]
        private void load(BackgroundImageStore imageStore)
        {
            Masking = true;
            CornerRadius = Radius;
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Colour4(70, 80, 90, 255),
                },
                new Container
                {
                    CornerRadius = Radius,
                    RelativeSizeAxes = Axes.X,
                    Height = Height * 0.9f,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = imageStore.Get(Texture_Name),
                        },
                        new Box
                        {
                            Colour = new Colour4(50, 50, 50, 255),
                            Alpha = 0.8f,
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                }
            };
            Action = action;
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            this.ScaleTo(0.8f, 1000, Easing.OutQuint);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            this.ScaleTo(1, 1000, Easing.OutQuint);
        }
    }
}
