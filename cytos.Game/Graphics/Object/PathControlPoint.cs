using System;
using cytos.Game.Graphics.Object.Types;
using Newtonsoft.Json;
using osu.Framework.Bindables;
using osuTK;

namespace cytos.Game.Graphics.Object
{
    public class PathControlPoint : IEquatable<PathControlPoint>
    {
        [JsonProperty]
        public readonly Bindable<Vector2> Position = new Bindable<Vector2>();

        [JsonProperty]
        public readonly Bindable<PathType?> Type = new Bindable<PathType?>();

        internal event Action Changed;

        public PathControlPoint()
        {
            Position.ValueChanged += _ => Changed?.Invoke();
            Type.ValueChanged += _ => Changed?.Invoke();
        }

        public PathControlPoint(Vector2 position, PathType? type = null)
            : this()
        {
            Position.Value = position;
            Type.Value = type;
        }

        public bool Equals(PathControlPoint other) => Position.Value == other?.Position.Value && Type.Value == other.Type.Value;
    }
}
