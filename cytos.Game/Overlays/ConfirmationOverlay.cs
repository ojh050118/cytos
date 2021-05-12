using System;
using cytos.Game.Graphics.UserInterface;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using osuTK.Input;

namespace cytos.Game.Overlays
{
    public class ConfirmationOverlay : CytosOverlay
    {
        private ConfirmationWindow currentWindow;

        public void Push(string text, Action onConfirm)
        {
            if (currentWindow != null)
                return;

            var window = new ConfirmationWindow(text, () =>
            {
                onConfirm?.Invoke();
                currentWindow = null;
                Hide();
            },
            () =>
            {
                currentWindow = null;
                Hide();
            });

            Add(window);

            currentWindow = window;

            Show();
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (currentWindow != null)
            {
                if (!e.Repeat)
                {
                    switch (e.Key)
                    {
                        case Key.Enter:
                            currentWindow.ForceConfirm();
                            return true;

                        case Key.Escape:
                            currentWindow.ForceDecline();
                            return true;
                    }
                };
            }

            return base.OnKeyDown(e);
        }

        private class ConfirmationWindow : CompositeDrawable
        {
            private const int content_offset = 20;

            private readonly Action confirm;
            private readonly Action decline;

            private readonly Box box;
            private readonly SpriteText textContainer;
            private readonly FillFlowContainer<BoxButton> buttonsContainer;

            public ConfirmationWindow(string text, Action onConfirm, Action onDecline)
            {
                confirm = () =>
                {
                    onConfirm?.Invoke();
                    animateHide();
                };

                decline = () =>
                {
                    onDecline?.Invoke();
                    animateHide();
                };

                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                RelativeSizeAxes = Axes.X;
                Height = 200;
                Masking = true;
                Alpha = 0;
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Shadow,
                    Radius = 9f,
                    Colour = Color4.Black.Opacity(0.4f),
                };
                AddRangeInternal(new Drawable[]
                {
                    box = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.DarkGray.Opacity(0.4f),
                        Size = new Vector2(0, 1)
                    },
                    new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.X,
                        RelativeSizeAxes = Axes.Y,
                        Padding = new MarginPadding {Vertical = 30},
                        Children = new Drawable[]
                        {
                            textContainer = new SpriteText
                            {
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                Text = text,
                                Alpha = 0,
                                Y = -content_offset,
                            },
                            buttonsContainer = new FillFlowContainer<BoxButton>
                            {
                                Anchor = Anchor.BottomCentre,
                                Origin = Anchor.BottomCentre,
                                Direction = FillDirection.Horizontal,
                                AutoSizeAxes = Axes.Both,
                                Spacing = new Vector2(50, 0),
                                Alpha = 0,
                                Y = content_offset,
                                Children = new[]
                                {
                                    new BoxButton(confirm, true),
                                    new BoxButton(decline)
                                }
                            }
                        }
                    }
                });
            }

            public void ForceConfirm() => confirm?.Invoke();

            public void ForceDecline() => decline?.Invoke();

            protected override void LoadComplete()
            {
                base.LoadComplete();
                animateShow();
            }

            private void animateShow()
            {
                box.ResizeWidthTo(1, 200, Easing.Out);

                textContainer.FadeIn(200, Easing.Out);
                textContainer.MoveToY(0, 200, Easing.Out);

                buttonsContainer.FadeIn(200, Easing.Out);
                buttonsContainer.MoveToY(0, 200, Easing.Out);

                this.FadeIn(200, Easing.Out);
            }

            private void animateHide()
            {
                box.ResizeWidthTo(0, 200, Easing.Out);

                textContainer.FadeOut(200, Easing.Out);
                textContainer.MoveToY(-content_offset, 200, Easing.Out);

                buttonsContainer.FadeOut(200, Easing.Out);
                buttonsContainer.MoveToY(content_offset, 200, Easing.Out);

                this.FadeOut(200, Easing.Out).Expire();
            }
        }
    }
}
