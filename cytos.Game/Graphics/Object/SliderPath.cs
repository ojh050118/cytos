using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cytos.Game.Graphics.Object.Types;
using Newtonsoft.Json;
using osu.Framework.Bindables;
using osu.Framework.Caching;
using osu.Framework.Utils;
using osuTK;

namespace cytos.Game.Graphics.Object
{
    public class SliderPath
    {
        [JsonIgnore]
        public IBindable<int> Version => version;

        private readonly Bindable<int> version = new Bindable<int>();

        public readonly BindableList<PathControlPoint> ControlPoints = new BindableList<PathControlPoint>();

        public readonly Bindable<double?> ExpectedDistance = new Bindable<double?>();

        private readonly List<Vector2> calculatedPath = new List<Vector2>();
        private readonly List<double> cumulativeLength = new List<double>();
        private readonly Cached pathCache = new Cached();

        private double calculatedLength;

        [JsonIgnore]
        public double Distance
        {
            get
            {
                ensureValid();
                return cumulativeLength.Count == 0 ? 0 : cumulativeLength[^1];
            }
        }

        private void ensureValid()
        {
            if (pathCache.IsValid)
                return;

            calculatePath();
            calculateLength();

            pathCache.Validate();
        }

        private void calculatePath()
        {
            calculatedPath.Clear();

            if (ControlPoints.Count == 0)
                return;

            Vector2[] vertices = new Vector2[ControlPoints.Count];
            for (int i = 0; i < ControlPoints.Count; i++)
                vertices[i] = ControlPoints[i].Position.Value;

            int start = 0;

            for (int i = 0; i < ControlPoints.Count; i++)
            {
                if (ControlPoints[i].Type.Value == null && i < ControlPoints.Count - 1)
                    continue;

                // The current vertex ends the segment
                var segmentVertices = vertices.AsSpan().Slice(start, i - start + 1);
                var segmentType = ControlPoints[start].Type.Value ?? PathType.Linear;

                foreach (Vector2 t in calculateSubPath(segmentVertices, segmentType))
                {
                    if (calculatedPath.Count == 0 || calculatedPath.Last() != t)
                        calculatedPath.Add(t);
                }

                // Start the new segment at the current vertex
                start = i;
            }
        }

        private List<Vector2> calculateSubPath(ReadOnlySpan<Vector2> subControlPoints, PathType type)
        {
            switch (type)
            {
                case PathType.Linear:
                    return PathApproximator.ApproximateLinear(subControlPoints);

                case PathType.PerfectCurve:
                    if (subControlPoints.Length != 3)
                        break;

                    List<Vector2> subpath = PathApproximator.ApproximateCircularArc(subControlPoints);

                    // If for some reason a circular arc could not be fit to the 3 given points, fall back to a numerically stable bezier approximation.
                    if (subpath.Count == 0)
                        break;

                    return subpath;

                case PathType.Catmull:
                    return PathApproximator.ApproximateCatmull(subControlPoints);
            }

            return PathApproximator.ApproximateBezier(subControlPoints);
        }

        private void calculateLength()
        {
            calculatedLength = 0;
            cumulativeLength.Clear();
            cumulativeLength.Add(0);

            for (int i = 0; i < calculatedPath.Count - 1; i++)
            {
                Vector2 diff = calculatedPath[i + 1] - calculatedPath[i];
                calculatedLength += diff.Length;
                cumulativeLength.Add(calculatedLength);
            }

            if (ExpectedDistance.Value is double expectedDistance && calculatedLength != expectedDistance)
            {
                // The last length is always incorrect
                cumulativeLength.RemoveAt(cumulativeLength.Count - 1);

                int pathEndIndex = calculatedPath.Count - 1;

                if (calculatedLength > expectedDistance)
                {
                    // The path will be shortened further, in which case we should trim any more unnecessary lengths and their associated path segments
                    while (cumulativeLength.Count > 0 && cumulativeLength[^1] >= expectedDistance)
                    {
                        cumulativeLength.RemoveAt(cumulativeLength.Count - 1);
                        calculatedPath.RemoveAt(pathEndIndex--);
                    }
                }

                if (pathEndIndex <= 0)
                {
                    // The expected distance is negative or zero
                    // TODO: Perhaps negative path lengths should be disallowed altogether
                    cumulativeLength.Add(0);
                    return;
                }

                // The direction of the segment to shorten or lengthen
                Vector2 dir = (calculatedPath[pathEndIndex] - calculatedPath[pathEndIndex - 1]).Normalized();

                calculatedPath[pathEndIndex] = calculatedPath[pathEndIndex - 1] + dir * (float)(expectedDistance - cumulativeLength[^1]);
                cumulativeLength.Add(expectedDistance);
            }
        }

        private void invalidate()
        {
            pathCache.Invalidate();
            version.Value++;
        }

        public SliderPath()
        {
            ExpectedDistance.ValueChanged += _ => invalidate();

            ControlPoints.CollectionChanged += (_, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (var c in args.NewItems.Cast<PathControlPoint>())
                            c.Changed += invalidate;
                        break;

                    case NotifyCollectionChangedAction.Reset:
                    case NotifyCollectionChangedAction.Remove:
                        foreach (var c in args.OldItems.Cast<PathControlPoint>())
                            c.Changed -= invalidate;
                        break;
                }

                invalidate();
            };
        }

        [JsonConstructor]
        public SliderPath(PathControlPoint[] controlPoints, double? expectedDistance = null)
            : this()
        {
            ControlPoints.AddRange(controlPoints);
            ExpectedDistance.Value = expectedDistance;
        }

        public SliderPath(PathType type, Vector2[] controlPoints, double? expectedDistance = null)
            : this(controlPoints.Select((c, i) => new PathControlPoint(c, i == 0 ? (PathType?)type : null)).ToArray(), expectedDistance)
        {
        }
    }
}
