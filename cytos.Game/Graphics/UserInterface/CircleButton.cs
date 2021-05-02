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
            get => icon;
            set => icon = value;
        }

        private IconUsage icon;
        public Action Action { get; set; }

        public CircleButton(Action action = null)
        {
            Action = action;
            Size = new Vector2(30);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            BorderThickness = 4;
            BorderColour = Color4.White;
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
                new SpriteIcon
                {
                    Icon = icon,
                    Size = new Vector2(Size.X / 2),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                }
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            this.ScaleTo(0.8f, 500, Easing.OutQuint);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            this.ScaleTo(1, 500, Easing.OutElastic);
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (Action != null)
                Action.Invoke();

            return base.OnClick(e);
        }

    }
}
