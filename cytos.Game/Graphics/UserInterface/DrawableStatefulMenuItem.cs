﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace cytos.Game.Graphics.UserInterface
{
    public class DrawableStatefulMenuItem : DrawableCytosMenuItem
    {
        protected new StatefulMenuItem Item => (StatefulMenuItem)base.Item;

        public DrawableStatefulMenuItem(StatefulMenuItem item)
            : base(item)
        {
        }

        protected override TextContainer CreateTextContainer() => new ToggleTextContainer(Item);

        private class ToggleTextContainer : TextContainer
        {
            private readonly StatefulMenuItem menuItem;
            private readonly Bindable<object> state;
            private readonly SpriteIcon stateIcon;

            public ToggleTextContainer(StatefulMenuItem menuItem)
            {
                this.menuItem = menuItem;

                state = menuItem.State.GetBoundCopy();

                Add(stateIcon = new SpriteIcon
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Size = new Vector2(10),
                    Margin = new MarginPadding { Horizontal = MARGIN_HORIZONTAL },
                    AlwaysPresent = true,
                });
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                state.BindValueChanged(updateState, true);
            }

            protected override void Update()
            {
                base.Update();

                stateIcon.X = BoldText.DrawWidth + 10;
            }

            private void updateState(ValueChangedEvent<object> state)
            {
                var icon = menuItem.GetIconForState(state.NewValue);

                if (icon == null)
                    stateIcon.Alpha = 0;
                else
                {
                    stateIcon.Alpha = 1;
                    stateIcon.Icon = icon.Value;
                }
            }
        }
    }
}
