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
    public class BoxButton : ClickableContainer
    {
        private string text;
        private Box hover;
        private SpriteText sprite;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public BoxButton(Action action, bool whiteBox = false)
        {
            Action = action;
            Size = new Vector2(60, 20);
            CornerRadius = 3;
            BorderThickness = 3;
            BorderColour = whiteBox ? Color4.White : Color4.Gray;
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = whiteBox ? Color4.White : new Color4(0, 0, 0, 0),
                    RelativeSizeAxes = Axes.Both
                },
                sprite = new SpriteText
                {
                    Colour = whiteBox ? Color4.Black : Color4.White,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Scale = new Vector2(0.9f),
                },
                hover = new Box
                {
                    Alpha = 0,
                    Colour = Color4.White,
                    RelativeSizeAxes = Axes.Both
                }
            };

            Action.Invoke();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Masking = true;
            sprite.Text = text;
        }


        protected override bool OnHover(HoverEvent e)
        {
            hover.FadeTo(0.5f, 300, Easing.In);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            hover.FadeTo(0, 300, Easing.In);
        }
    }
}
