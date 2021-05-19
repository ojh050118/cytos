using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Graphics.UserInterface
{
    public class CircleButton : CircularContainer
    {
        public IconUsage Icon
        {
            get => icon.Icon;
            set => icon.Icon = value;
        }

        private readonly SpriteIcon icon;
        public Action Action { get; set; }
        private readonly Container content;

        public CircleButton(Action action = null)
        {
            Action = action;
            Size = new Vector2(30);
            Children = new Drawable[]
            {
                content = new CircularContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    BorderThickness = 3,
                    BorderColour = Color4.White,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Color4.Black,
                            Alpha = 0.5f,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                        },
                        icon = new SpriteIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            icon.Size = new Vector2(Size.X / 2);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            content.ScaleTo(0.8f, 700, Easing.OutQuint);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            content.ScaleTo(1, 700, Easing.OutElastic);
        }

        protected override bool OnClick(ClickEvent e)
        {
            Action?.Invoke();

            return base.OnClick(e);
        }
    }
}
