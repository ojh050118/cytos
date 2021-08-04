using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using osu.Framework.Platform;
using osu.Framework.Threading;
using osuTK;

namespace cytos.Game.Beatmap
{
    public class BeatmapStorage
    {
        private readonly Storage storage;
        private readonly Scheduler scheduler;


        public BeatmapStorage(Scheduler scheduler, Storage storage)
        {
            this.scheduler = scheduler;
            this.storage = storage?.GetStorageForDirectory("beatmaps");
        }

        /// <summary>
        /// 비트맵 정보와 파일 이름들이 있는 리스트 반환
        /// </summary>
        /// <returns></returns>
        public List<(BeatmapInfo, string)> GetBeatmaps()
        {
            List<(BeatmapInfo, string)> beatmaps = new List<(BeatmapInfo, string)>();

            foreach (var dir in storage.GetFiles(string.Empty))
            {
                var file = dir;

                if (File.Exists(file))
                    continue;

                StreamReader sr = File.OpenText(storage.GetFullPath(string.Empty) + $"/{file}");
                var text = sr.ReadLine();
                sr.Close();

                var beatmap = JsonConvert.DeserializeObject<BeatmapInfo>(text);
                var name = beatmap.Title;

                beatmaps.Add((beatmap, name));
            }

            return beatmaps;
        }

        public void CreateBeatmap(string name, BeatmapInfo info)
        {
            string json = JsonConvert.SerializeObject(info);

            StreamWriter sw = File.CreateText(storage.GetFullPath(string.Empty) + $"/{name}");
            sw.WriteLine(json);
            sw.Close();
        }

        public void DeleteBeatmap(string name) => Directory.Delete(storage.GetFullPath(string.Empty) + $"/{name}");

        public BeatmapInfo GetBeatmapInfo(string name)
        {
            BeatmapInfo info = new BeatmapInfo();

            foreach (var file in storage.GetFiles(string.Empty))
            {
                if (!name.Equals(file))
                    continue;

                StreamReader sr = File.OpenText(storage.GetFullPath(string.Empty) + $"/{file}");
                var text = sr.ReadLine();
                sr.Close();
                info = JsonConvert.DeserializeObject<BeatmapInfo>(text);
            }

            return info;
        }
    }

    public struct BeatmapInfo
    {
        public string Title;
        public string Author;
        public string Background;
        public string Track;
        public float BPM;
        public Notes[] Notes;
    }
    public struct Notes
    {
        public NoteType NoteType;
        public float StartTime;
        public Vector2[] Positions;
    }

    public enum NoteType
    {
        Click,
        Drag,
        Hold,
        Swipe,
        Overpage,
        Flick
    }
}
