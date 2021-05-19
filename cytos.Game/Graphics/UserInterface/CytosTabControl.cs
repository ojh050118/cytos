﻿using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Graphics.UserInterface
{
    public class CytosTabControl<T> : TabControl<T>
    {
        public const float SPACING = 10;

        private readonly Box strip;

        protected override Dropdown<T> CreateDropdown() => new CytosTabDropdown<T>();

        protected override TabItem<T> CreateTabItem(T value) => new CytosTabItem(value);

        protected virtual float StripWidth => TabContainer.Children.Sum(c => c.IsPresent ? c.DrawWidth + TabContainer.Spacing.X : 0) - TabContainer.Spacing.X;

        protected virtual bool AddEnumEntriesAutomatically => true;

        private static bool isEnumType => typeof(T).IsEnum;

        public CytosTabControl()
        {
            TabContainer.Spacing = new Vector2(SPACING , 0);

            AddInternal(strip = new Box
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                Height = 1,
                Colour = Color4.White,
            });

            if (isEnumType && AddEnumEntriesAutomatically)
            {
                foreach (var val in (T[])Enum.GetValues(typeof(T)))
                    AddItem(val);
            }
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            if (strip.Colour.MaxAlpha > 0)
                strip.Width = Interpolation.ValueAt(Math.Clamp(Clock.ElapsedFrameTime, 0, 1000), strip.Width, StripWidth, 0, 500, Easing.OutQuint);
        }

        public class CytosTabItem : TabItem<T>
        {
            protected readonly SpriteText Text;
            protected readonly Box Bar;

            private const float transition_length = 500;

            protected void FadeHovered()
            {
                Bar.FadeIn(transition_length, Easing.OutQuint);
                Text.FadeColour(Color4.White, transition_length, Easing.OutQuint);
            }

            protected void FadeUnhovered()
            {
                Bar.FadeTo(IsHovered ? 1 : 0, transition_length, Easing.OutQuint);
                Text.FadeColour(IsHovered ? Color4.White : new Color4(100, 100, 100, 255), transition_length, Easing.OutQuint);
            }

            protected override bool OnHover(HoverEvent e)
            {
                if (!Active.Value)
                    FadeHovered();
                return true;
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                if (!Active.Value)
                    FadeUnhovered();
            }

            public CytosTabItem(T value)
                : base(value)
            {
                AutoSizeAxes = Axes.X;
                RelativeSizeAxes = Axes.Y;

                Children = new Drawable[]
                {
                    Text = new SpriteText
                    {
                        Margin = new MarginPadding { Top = 5, Bottom = 5 },
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                        Text = (value as IHasDescription)?.Description ?? (value as Enum)?.GetDescription() ?? value.ToString(),
                        Font = FontUsage.Default.With(size: 14),
                        Colour = new Color4(100, 100, 100, 255)
                    },
                    Bar = new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 1,
                        Alpha = 0,
                        Colour = Color4.White,
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                    },
                };
            }

            protected override void OnActivated()
            {
                FadeHovered();
            }

            protected override void OnDeactivated()
            {
                FadeUnhovered();
            }
        }
    }
}
