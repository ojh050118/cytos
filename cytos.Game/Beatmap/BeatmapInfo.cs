using System;
using System.Collections.Generic;
using osuTK;

namespace cytos.Game.Beatmap
{
    [Serializable]
    public class BeatmapInfo
    {
        public BeatmapMetadata Metadata;

        public List<Vector2> Positions;
    }
}
