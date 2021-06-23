using System;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK.Input;

namespace cytos.Game.Graphics.Containers
{
    public class ImageContainer : ClickableContainer
    {
        public string TextureName { get; }

        public static int Radius = 10;

        private readonly Action action;

        private CytosMenu menu;

        public ImageContainer(string texture, Action action = null)
        {
            TextureName = texture;
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
                    RelativeSizeAxes = Axes.Both,
                    Height = 0.9f,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = imageStore.Get(TextureName),
                        },
                        new Box
                        {
                            Colour = new Colour4(50, 50, 50, 255),
                            Alpha = 0.8f,
                            RelativeSizeAxes = Axes.Both
                        },
                        menu = new CytosMenu(Direction.Vertical, true)
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.X,
                            State = MenuState.Closed,
                            Items = new CytosMenuItem[]
                            {
                                new CytosMenuItem("Play"),
                                new CytosMenuItem("Edit", MenuItemType.Highlighted),
                                new CytosMenuItem("Delete", MenuItemType.Destructive),
                            }
                        }
                    }
                },
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    Height = 0.1f,
                    Padding = new MarginPadding(5),
                    Child = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = Name
                    }
                }
            };
            Action = action;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            switch (e.Button)
            {
                case MouseButton.Left:
                    menu.Open();
                    this.ScaleTo(0.8f, 1000, Easing.OutQuint);
                    break;
                case MouseButton.Right:
                    menu.Close();
                    break;
            }

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            this.ScaleTo(1, 1000, Easing.OutQuint);
        }

        protected override bool OnClick(ClickEvent e)
        {
            switch (e.Button)
            {
                case MouseButton.Left:
                    menu.Open();
                    break;
                case MouseButton.Right:
                    menu.Close();
                    break;
            }

            return base.OnClick(e);
        }
    }
}
