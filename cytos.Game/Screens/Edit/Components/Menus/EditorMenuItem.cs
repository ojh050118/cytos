using System;
using cytos.Game.Graphics.UserInterface;

namespace cytos.Game.Screens.Edit.Components.Menus
{
    public class EditorMenuItem : CytosMenuItem
    {
        private const int min_text_length = 40;

        public EditorMenuItem(string text, MenuItemType type = MenuItemType.Standard)
            : base(text.PadRight(min_text_length), type)
        {
        }

        public EditorMenuItem(string text, MenuItemType type, Action action)
            : base(text.PadRight(min_text_length), type, action)
        {
        }
    }
}
