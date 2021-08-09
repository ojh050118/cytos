using System.Linq;
using cytos.Game.Beatmap;
using cytos.Game.Graphics.Object;
using cytos.Game.Screens.Play;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;

namespace cytos.Game.Screens.Edit.Compose
{
    public class ComposeScreen : EditModeScreen
    {
        private BeatmapInfo info;
        private ScreenStack field;
        private Container noteArea;

        public ComposeScreen()
            : base(EditMode.Compose)
        {
        }

        public ComposeScreen(BeatmapInfo info)
            : base(EditMode.Compose)
        {
            this.info = info;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Padding = new MarginPadding(50);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Masking = true,
                    CornerRadius = 15,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        field = new ScreenStack(),
                        new noteField()
                    }
                }
            };

            field.Push(new Playfield(EditorScreen.CurrentInfo, true)
            {
                RelativeSizeAxes = Axes.Both
            });

        }

        private class noteField : Container
        {
            private InputManager input;

            [BackgroundDependencyLoader]
            private void load()
            {
                RelativeSizeAxes = Axes.Both;
                Padding = new MarginPadding { Vertical = 50 };

                foreach (var note in EditorScreen.CurrentInfo.Notes)
                {
                    if (note.NoteType == NoteType.Click)
                    {
                        Add(new ClickNote
                        {
                            Position = note.Positions[0]
                        });
                    }
                }
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                input = GetContainingInputManager();
            }


            protected override bool OnClick(ClickEvent e)
            {
                var sizeDiv2 = DrawSize / 2;
                ClickNote obj;

                // 원점은 화면에서 중심. 따라서 오브젝트의 위치중에서 음수 값이 나올 수 있음.
                Add(obj = new ClickNote
                {
                    Position = ToLocalSpace(input.CurrentState.Mouse.Position) - sizeDiv2
                });
                EditorScreen.CurrentInfo.Notes.Add(new Notes
                {
                    NoteType = NoteType.Click,
                    Positions = new Vector2[] { obj.Position },
                    StartTime = EditorScreen.CurrentTime.Value - 1000
                });

                return base.OnClick(e);
            }

            protected override bool OnKeyDown(KeyDownEvent e)
            {
                switch (e.Key)
                {
                    case Key.W:
                        if (Children.Count != 0)
                        {
                            Remove(Children.Last());
                            EditorScreen.CurrentInfo.Notes.Remove(EditorScreen.CurrentInfo.Notes.Last());
                        }
                        break;
                }

                return base.OnKeyDown(e);
            }
        }
    }
}
