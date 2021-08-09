using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;

namespace cytos.Game.Input
{
    public class CytosKeyBindingContainer : KeyBindingContainer<InputAction>
    {
        private readonly Drawable handler;
        private InputManager parentInputManager;

        public override IEnumerable<IKeyBinding> DefaultKeyBindings => GlobalKeyBindings;

        public IEnumerable<KeyBinding> GlobalKeyBindings => new[]
        {
            new KeyBinding(new[] { InputKey.Control, InputKey.R }, InputAction.Reload),
            new KeyBinding(new[] { InputKey.Control, InputKey.S }, InputAction.Save),
        };

        public CytosKeyBindingContainer(CytosGameBase game)
            : base(matchingMode: KeyCombinationMatchingMode.Modifiers)
        {
            if (game is IKeyBindingHandler<InputAction>)
                handler = game;
        }

        protected override IEnumerable<Drawable> KeyBindingInputQueue
        {
            get
            {
                var inputQueue = parentInputManager?.NonPositionalInputQueue ?? base.KeyBindingInputQueue;

                return handler != null ? inputQueue.Prepend(handler) : inputQueue;
            }
        }
    }
}
