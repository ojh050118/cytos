using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace cytos.Game.Graphics.UserInterface
{
    public class CytosTextFlowContainer : TextFlowContainer
    {
        public CytosTextFlowContainer(Action<SpriteText> defaultCreationParameters = null)
            : base(defaultCreationParameters)
        {
        }

        protected override SpriteText CreateSpriteText() => new();

        public void AddArbitraryDrawable(Drawable drawable) => AddInternal(drawable);

        public IEnumerable<Drawable> AddIcon(IconUsage icon, Action<SpriteText> creationParameters = null) => AddText(icon.Icon.ToString(), creationParameters);
    }
}
