using cytos.Game.Configuration;
using cytos.Game.IO;
using cytos.Resources;
using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osuTK;

namespace cytos.Game
{
    public class CytosGameBase : osu.Framework.Game
    {
        protected override Container<Drawable> Content { get; }
        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public bool IsDevelopmentBuild { get; private set; }

        protected Storage Storage { get; private set; }

        protected CytosConfigManager LocalConfig;

        private BeatmapAudioManager beatmapAudioManager;

        protected CytosGameBase()
        {
            IsDevelopmentBuild = DebugUtils.IsDebugBuild;

            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1366, 768)
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(CytosResources).Assembly));
            var largeStore = new LargeTextureStore(Host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, @"Textures")));
            largeStore.AddStore(Host.CreateTextureLoaderStore(new OnlineStore()));

            AddFont(Resources, @"Fonts/Electrolize");

            var files = Storage.GetStorageForDirectory("files");
            var tracks = new ResourceStore<byte[]>();
            tracks.AddStore(new TrackStore(files));
            beatmapAudioManager = new BeatmapAudioManager(Host.AudioThread, tracks, new ResourceStore<byte[]>()) { EventScheduler = Scheduler };

            dependencies.Cache(beatmapAudioManager);
            dependencies.CacheAs(new BackgroundImageStore(Scheduler, files));
            dependencies.Cache(largeStore);
            dependencies.CacheAs(this);
            dependencies.CacheAs(LocalConfig);

            Host.Window.Title = "cytos";
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            var fpsDisplayBindable = LocalConfig.GetBindable<bool>(CytosSetting.FpsOverlay);
            fpsDisplayBindable.ValueChanged += val => { FrameStatistics.Value = val.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None; };
            fpsDisplayBindable.TriggerChange();

            FrameStatistics.ValueChanged += e => fpsDisplayBindable.Value = e.NewValue != FrameStatisticsMode.None;
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Storage ??= host.Storage;
            LocalConfig ??= new CytosConfigManager(Storage);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            LocalConfig?.Dispose();
        }
    }
}
