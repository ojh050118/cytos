using System;

namespace cytos.Game.Beatmap
{
    [Serializable]
    public class BeatmapMetadata
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public float BPM { get; set; }
    }
}
