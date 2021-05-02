using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Allocation;

namespace cytos.Game.Graphics.UserInterface
{
    public class OverlayHeader : Container
    {
        public bool Expansion { get; private set; } = true;
        private Box box;
        private Circle lCircle;
        private Circle rCircle;

        /// <summary>
        /// 상대크기 축: X, 자동 크기 축: Y
        /// </summary>
        public OverlayHeader(bool startExpanded = true)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Expansion = startExpanded;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
            Margin = new MarginPadding { Top = 5, Bottom = 5 };
            Children = new Drawable[]
            {
                lCircle = new Circle
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.Centre,
                    Colour = Color4.White,
                    Size = new Vector2(9),
                    Position = new Vector2(X - 10, Y)
                },
                box = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    Colour = Color4.White,
                    Height = 1.5f
                },
                rCircle = new Circle
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.Centre,
                    Colour = Color4.White,
                    Size = new Vector2(9),
                    Position = new Vector2(X + 10, Y)
                },
            };
        }

        public void ToggleExpansion()
        {
            ClearTransforms();
            lCircle.ClearTransforms();
            rCircle.ClearTransforms();

            if (Expansion)
            {
                this.ResizeWidthTo(0, 500, Easing.OutQuint);
                lCircle.MoveToX(0, 550, Easing.OutQuint);
                rCircle.MoveToX(0, 550, Easing.OutQuint);
            }
            else
            {
                this.ResizeWidthTo(1, 500, Easing.OutQuint);
                lCircle.MoveToX(-10, 500, Easing.OutQuint);
                rCircle.MoveToX(10, 500, Easing.OutQuint);
            }

            Expansion = !Expansion;
        }
    }
}
