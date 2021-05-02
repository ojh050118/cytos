﻿using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Graphics.UserInterface
{
    public class LoadingSpinner : VisibilityContainer
    {
        private readonly SpriteIcon spinner;

        protected override bool StartHidden => true;

        protected Container MainContents;

        public const float TRANSITION_DURATION = 500;

        private const float spin_duration = 900;

        /// <summary>
        /// Constuct a new loading spinner.
        /// </summary>
        /// <param name="withBox">스피너에 가시성을 위해 주변에 검은 색 상자가 있어야하는지 여부.</param>
        /// <param name="inverted">색상 반전 여부 (흰색 대신 검은 색 스피너).</param>
        public LoadingSpinner(bool withBox = false, bool inverted = false)
        {
            Size = new Vector2(60);

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Child = MainContents = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 20,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Colour = inverted ? Color4.White : Color4.Black,
                        RelativeSizeAxes = Axes.Both,
                        Alpha = withBox ? 0.7f : 0
                    },
                    spinner = new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = inverted ? Color4.Black : Color4.White,
                        Scale = new Vector2(withBox ? 0.6f : 1),
                        RelativeSizeAxes = Axes.Both,
                        Icon = FontAwesome.Solid.CircleNotch
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            rotate();
        }

        protected override void Update()
        {
            base.Update();

            MainContents.CornerRadius = MainContents.DrawWidth / 4;
        }

        protected override void PopIn()
        {
            if (Alpha < 0.5f)
                rotate();

            MainContents.ScaleTo(1, TRANSITION_DURATION, Easing.OutQuint);
            this.FadeIn(TRANSITION_DURATION * 2, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            MainContents.ScaleTo(0.8f, TRANSITION_DURATION / 2, Easing.In);
            this.FadeOut(TRANSITION_DURATION, Easing.OutQuint);
        }

        private void rotate()
        {
            spinner.Spin(spin_duration * 3.5f, RotationDirection.Clockwise);

            MainContents.RotateTo(0).Then()
                        .RotateTo(90, spin_duration, Easing.InOutQuart).Then()
                        .RotateTo(180, spin_duration, Easing.InOutQuart).Then()
                        .RotateTo(270, spin_duration, Easing.InOutQuart).Then()
                        .RotateTo(360, spin_duration, Easing.InOutQuart).Then()
                        .Loop();
        }
    }
}
