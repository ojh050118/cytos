using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace cytos.Game.Configuration
{
    public class CytosConfigManager : IniConfigManager<CytosSetting>
    {
        protected override string Filename => @"config.ini";

        public CytosConfigManager(Storage storage)
            : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            SetValue(CytosSetting.Volume, 1.0f);
            SetValue(CytosSetting.FpsOverlay, false);
            SetValue(CytosSetting.BackgroundImageFile, string.Empty);
            SetValue(CytosSetting.BackgroundTrackFile, string.Empty);
        }
    }

    public enum CytosSetting
    {
        Volume,
        FpsOverlay,
        BackgroundImageFile,
        BackgroundTrackFile
    }
}
