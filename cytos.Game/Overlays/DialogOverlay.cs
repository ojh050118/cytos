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
    public class DialogOverlay : CytosOverlay
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
                }
            }

            return base.OnKeyDown(e);
        }

        private class ConfirmationWindow : CompositeDrawable
        {
            private const int content_offset = 20;

            private readonly Action confirm;
            private readonly Action decline;
            private readonly FillFlowContainer content;

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

                Anchor = Anchor.BottomCentre;
                Origin = Anchor.BottomCentre;
                RelativeSizeAxes = Axes.Y;
                Width = 600;
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
                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.DarkGray.Opacity(0.4f),
                    },
                    new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Alpha = 0.1f,
                        Colour = Color4.White,
                        Icon = FontAwesome.Solid.ExclamationTriangle,
                        Size = new Vector2(Width * 0.5f),
                    },
                    content = new FillFlowContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding { Vertical = 30, Horizontal = 20 },
                        Spacing = new Vector2(0, 20),
                        Alpha = 0,
                        Y = 200,
                        Children = new Drawable[]
                        {
                            new CytosTextFlowContainer(s => s.Font = FontUsage.Default.With(size: 30))
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Text = text,
                            },
                            new FillFlowContainer<BoxButton>
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Direction = FillDirection.Vertical,
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Spacing = new Vector2(0, 5),
                                Y = content_offset,
                                Children = new[]
                                {
                                    new BoxButton(confirm, true)
                                    {
                                        Text = "YES",
                                        RelativeSizeAxes = Axes.X
                                    },
                                    new BoxButton(decline)
                                    {
                                        Text = "NO",
                                        RelativeSizeAxes = Axes.X
                                    }
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
                content.FadeIn(200, Easing.Out);
                content.MoveToY(0, 300, Easing.OutQuint);

                this.FadeIn(200, Easing.Out);
            }

            private void animateHide()
            {
                content.FadeOut(200, Easing.Out);
                content.MoveToY(200, 300, Easing.OutQuint);

                this.FadeOut(200, Easing.Out).Expire();
            }
        }
    }
}
