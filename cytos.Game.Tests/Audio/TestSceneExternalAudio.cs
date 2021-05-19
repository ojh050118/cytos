using cytos.Game.IO;
using cytos.Game.Tests.Visual;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;

namespace cytos.Game.Tests.Audio
{
    public class TestSceneExternalAudio : cytosTestScene
    {
        private Track track;

        [BackgroundDependencyLoader]
        private void load(BeatmapAudioManager beatmapAudio)
        {
            track = beatmapAudio.Tracks.Get("sample-track");
            AddStep("Play", () => track.Start());
            AddStep("Stop", () => track.Stop());
        }

    }
}
