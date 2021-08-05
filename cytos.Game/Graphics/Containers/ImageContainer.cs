using System;
using System.IO;
using cytos.Game.Beatmap;
using cytos.Game.Graphics.UserInterface;
using cytos.Game.IO;
using cytos.Game.Screens.Edit;
using cytos.Game.Screens.Play;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK.Input;

namespace cytos.Game.Graphics.Containers
{
    public class ImageContainer : ClickableContainer
    {
        [Resolved]
        private Storage storage { get; set; }

        public string TextureName { get; }

        public static int Radius = 10;

        /// <summary>
        /// 매뉴를 클릭했을 때 액션.
        /// RequestedNewScreen의 값이 변경되고 호출됩니다.
        /// </summary>
        public Action ClickedAction;

        private CytosMenu menu;

        private string fileName;

        public IScreen RequestedNewScreen;

        public Action DeleteAction { get; init; }

        public ImageContainer(string texture, string name)
        {
            TextureName = texture;
            fileName = name;
        }

        [BackgroundDependencyLoader]
        private void load(BackgroundImageStore imageStore, BeatmapStorage beatmaps)
        {
            Masking = true;
            CornerRadius = Radius;
            var path = storage.GetStorageForDirectory("files").GetStorageForDirectory("beatmaps");
            BeatmapInfo info = beatmaps.GetBeatmapInfo(fileName);
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
                                // Todo: 액션 추가
                                new CytosMenuItem("Play", MenuItemType.Standard, () =>
                                {
                                    RequestedNewScreen = new Playfield(info.Background);
                                    ClickedAction.Invoke();
                                }),
                                new CytosMenuItem("Edit", MenuItemType.Highlighted, () =>
                                {
                                    RequestedNewScreen = new EditorScreen(beatmaps.GetBeatmapInfo(fileName));
                                    ClickedAction.Invoke();
                                }),
                                new CytosMenuItem("Delete", MenuItemType.Destructive, () =>
                                {
                                    File.Delete(path.GetFullPath(string.Empty) + @"\" + Name);
                                    DeleteAction.Invoke();
                                }),
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
