using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace cytos.Game.Beatmap
{
    public static class BeatmapFile
    {
        public static List<(BeatmapInfo, string)> GetBeatmaps()
        {
            List<(BeatmapInfo, string)> beatmaps = new List<(BeatmapInfo, string)>();

            if (!checkMainDirectoryExistance())
                return beatmaps;

            var files = Directory.GetFiles("beatmaps/");

            if (files.Length == 0)
                return beatmaps;

            foreach (var f in files)
            {
                using (StreamReader sr = File.OpenText(f))
                {
                    var text = sr.ReadLine();
                    sr.Close();

                    var beatmap = JsonConvert.DeserializeObject<BeatmapInfo>(text);
                    var name = "test";

                    beatmaps.Add((beatmap, name));
                }
            }

            return beatmaps;
        }

        public static void CreateBeatmap(string name, BeatmapInfo beatmap)
        {
            string jsonResult = JsonConvert.SerializeObject(beatmap);

            using (StreamWriter sw = File.CreateText($"beatmaps/{name}"))
            {
                sw.WriteLine(jsonResult.ToString());
                sw.Close();
            }
        }

        private static bool checkMainDirectoryExistance()
        {
            if (!Directory.Exists("beatmaps"))
            {
                Directory.CreateDirectory("beatmaps");
                return false;
            }

            return true;
        }
    }
}
