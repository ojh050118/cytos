using System;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.Screens.Edit.Setup;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace cytos.Game.Screens.Edit
{
    public class EditorScreen : CytosScreen
    {
        private Container screenContainer;
        private Action addScreen;
        private CytosTabControl<EditMode> tabControl;

        public EditorScreen(Drawable drawable = null)
        {
            if (drawable is not null)
                addScreen = () => screenContainer.Add(drawable);
            else
            {
                addScreen = () => screenContainer.Add(new ScreenStack(new SetupScreen()));
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 40,
                    Name = "Menu + Sections",
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black
                        },
                        new Container
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            RelativeSizeAxes = Axes.Y,
                            AutoSizeAxes = Axes.X,
                            Padding = new MarginPadding { Bottom = 5, Left = 10 },
                            Child = tabControl = new CytosTabControl<EditMode>
                            {
                                Anchor = Anchor.CentreRight,
                                Origin = Anchor.CentreRight,
                                RelativeSizeAxes = Axes.Y,
                                Width = 200,
                                AutoSort = true
                            }
                        }

                    }
                },
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Height = 80,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(20, 20, 20, 255)
                        },
                    }
                },
                screenContainer = new Container
                {
                    Padding = new MarginPadding { Top = 40, Bottom = 80 },
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Scale = new Vector2(0.8f),
                    Name = "Screen Container"
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            tabControl.PinItem(EditMode.Setup);
            tabControl.PinItem(EditMode.Compose);
            tabControl.PinItem(EditMode.Timing);
            screenContainer.ScaleTo(1, 300, Easing.OutQuint);
            addScreen.Invoke();
        }
    }

    public enum EditMode
    {
        Setup,
        Compose,
        Timing
    }
}
