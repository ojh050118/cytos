using System;
using osu.Framework.Graphics.UserInterface;

namespace cytos.Game.Graphics.UserInterface
{
    public class CytosMenuItem : MenuItem
    {
        public readonly MenuItemType Type;

        public CytosMenuItem(string text, MenuItemType type = MenuItemType.Standard)
            : this(text, type, null)
        {
        }

        public CytosMenuItem(string text, MenuItemType type, Action action)
            : base(text, action)
        {
            Type = type;
        }
    }
}
