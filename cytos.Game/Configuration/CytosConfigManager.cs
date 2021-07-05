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
            SetValue(CytosSetting.BlurAmount, 0);
        }
    }

    public enum CytosSetting
    {
        Volume,
        FpsOverlay,
        BlurAmount
    }
}
