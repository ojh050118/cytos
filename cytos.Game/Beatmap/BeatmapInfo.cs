﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
