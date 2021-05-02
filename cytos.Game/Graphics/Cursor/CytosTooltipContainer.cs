﻿using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Graphics.Cursor
{
    public class CytosTooltipContainer : TooltipContainer
    {
        protected override ITooltip CreateTooltip() => new CytosTooltip();

        public CytosTooltipContainer(CursorContainer cursor)
            : base(cursor)
        {
        }

        protected override double AppearDelay => (1 - CurrentTooltip.Alpha) * base.AppearDelay;

        public class CytosTooltip : Tooltip
        {
            private readonly Box background;
            private readonly SpriteText text;
            private bool instantMovement = true;

            public override bool SetContent(object content)
            {
                if (!(content is string contentString))
                    return false;

                if (contentString == text.Text) return true;

                text.Text = contentString;

                if (IsPresent)
                {
                    AutoSizeDuration = 250;
                    //background.FlashColour(OsuColour.Gray(0.4f), 1000, Easing.OutQuint);
                }
                else
                    AutoSizeDuration = 0;

                return true;
            }

            public CytosTooltip()
            {
                AutoSizeEasing = Easing.OutQuint;

                CornerRadius = 5;
                Masking = true;
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Shadow,
                    Colour = Color4.Black.Opacity(40),
                    Radius = 5,
                };
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0.9f,
                    },
                    text = new SpriteText
                    {
                        Padding = new MarginPadding(5),
                    }
                };
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                background.Colour = Color4.Gray;
            }

            protected override void PopIn()
            {
                instantMovement |= !IsPresent;
                this.FadeIn(500, Easing.OutQuint);
            }

            protected override void PopOut() => this.Delay(150).FadeOut(500, Easing.OutQuint);

            public override void Move(Vector2 pos)
            {
                if (instantMovement)
                {
                    Position = pos;
                    instantMovement = false;
                }
                else
                {
                    this.MoveTo(pos, 200, Easing.OutQuint);
                }
            }
        }
    }
}