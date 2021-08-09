using System.Collections.Generic;
using cytos.Game.Graphics.Object.Types;
using Newtonsoft.Json;
using osu.Framework.Bindables;

namespace cytos.Game.Graphics.Object
{
    public class HitObject
    {
        public readonly Bindable<double> StartTimeBindable = new BindableDouble();

        private readonly List<HitObject> nestedHitObjects = new List<HitObject>();

        [JsonIgnore]
        public IReadOnlyList<HitObject> NestedHitObjects => nestedHitObjects;

        /// <summary>
        /// HitObject가 시작되는 시간.
        /// </summary>
        public virtual double StartTime
        {
            get => StartTimeBindable.Value;
            set => StartTimeBindable.Value = value;
        }

        public HitObject()
        {
            StartTimeBindable.ValueChanged += time =>
            {
                double offset = time.NewValue - time.OldValue;

                foreach (var nested in nestedHitObjects)
                    nested.StartTime += offset;
            };
        }

        protected void AddNested(HitObject hitObject) => nestedHitObjects.Add(hitObject);
    }

    public static class HitObjectExtensions
    {
        public static double GetEndTime(this HitObject hitObject) => (hitObject as IHasDuration)?.EndTime ?? hitObject.StartTime;
    }
}
