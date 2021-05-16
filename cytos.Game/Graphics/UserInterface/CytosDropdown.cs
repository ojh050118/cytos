using System.Linq;
using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;

namespace cytos.Game.Graphics.UserInterface
{
    public class CytosDropdown<T> : Dropdown<T>
    {
        private const float corner_radius = 4;

        protected override DropdownHeader CreateHeader() => new CytosDropdownHeader();

        protected override DropdownMenu CreateMenu() => new CytosDropdownMenu();

        #region CytosDropdownHeader

        public class CytosDropdownHeader : DropdownHeader
        {
            protected readonly SpriteText Text;
            protected readonly SpriteIcon Icon;

            protected override LocalisableString Label
            {
                get => Text.Text;
                set => Text.Text = value;
            }

            public CytosDropdownHeader()
            {
                Foreground.Padding = new MarginPadding(4);

                AutoSizeAxes = Axes.None;
                Margin = new MarginPadding { Bottom = 4 };
                CornerRadius = corner_radius;
                Height = 40;

                Foreground.Children = new Drawable[]
                {
                    Text = new SpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                    Icon = new SpriteIcon
                    {
                        Icon = FontAwesome.Solid.ChevronDown,
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Margin = new MarginPadding { Right = 5 },
                        Size = new Vector2(12),
                    },
                };
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                BackgroundColour = Color4.Black.Opacity(0.5f);
            }
        }

        #endregion

        #region CytosDropdownMenu

        public class CytosDropdownMenu : DropdownMenu
        {
            public override bool HandleNonPositionalInput => State == MenuState.Open;

            public CytosDropdownMenu()
            {
                CornerRadius = corner_radius;
                BackgroundColour = Color4.Black.Opacity(0.5f);

                MaskingContainer.CornerRadius = corner_radius;

                ItemsContainer.Padding = new MarginPadding(5);
            }

            protected override void AnimateOpen() => this.FadeIn(300, Easing.OutQuint);
            protected override void AnimateClose() => this.FadeOut(300, Easing.OutQuint);

            protected override void UpdateSize(Vector2 newSize)
            {
                if (Direction == Direction.Vertical)
                {
                    Width = newSize.X;
                    this.ResizeHeightTo(newSize.Y, 300, Easing.OutQuint);
                }
                else
                {
                    Height = newSize.Y;
                    this.ResizeWidthTo(newSize.X, 300, Easing.OutQuint);
                }
            }

            protected override Menu CreateSubMenu() => new CytosMenu(Direction.Vertical);

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawableCytosDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new BasicScrollContainer(direction);

            #region DrawableCytosDropdownMenuItem

            public class DrawableCytosDropdownMenuItem : DrawableDropdownMenuItem
            {
                public override bool HandlePositionalInput => true;

                public DrawableCytosDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    Foreground.Padding = new MarginPadding(2);
                    Masking = true;
                    CornerRadius = corner_radius;
                }

                protected override void UpdateForegroundColour()
                {
                    base.UpdateForegroundColour();

                    if (Foreground.Children.FirstOrDefault() is Content content) content.Chevron.Alpha = IsHovered ? 1 : 0;
                }

                protected override Drawable CreateContent() => new Content();

                protected new class Content : FillFlowContainer, IHasText
                {
                    public readonly SpriteText Label;
                    public readonly SpriteIcon Chevron;

                    public LocalisableString Text
                    {
                        get => Label.Text;
                        set => Label.Text = value;
                    }

                    public Content()
                    {
                        RelativeSizeAxes = Axes.X;
                        AutoSizeAxes = Axes.Y;
                        Direction = FillDirection.Horizontal;

                        Children = new Drawable[]
                        {
                            Chevron = new SpriteIcon
                            {
                                AlwaysPresent = true,
                                Icon = FontAwesome.Solid.ChevronRight,
                                Colour = Color4.Black,
                                Alpha = 0.5f,
                                Size = new Vector2(8),
                                Margin = new MarginPadding { Left = 3, Right = 3 },
                                Origin = Anchor.CentreLeft,
                                Anchor = Anchor.CentreLeft,
                            },
                            Label = new SpriteText
                            {
                                Origin = Anchor.CentreLeft,
                                Anchor = Anchor.CentreLeft,
                            },
                        };
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}
