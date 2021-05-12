using cytos.Game.IO.Monitors;
using osu.Framework.Platform;
using osu.Framework.Threading;

namespace cytos.Game.IO
{
    public abstract class BackgroundStore<T> : MonitoredDirectoryStore<T>
        where T : class
    {
        protected override string DirectoryName => @"backgrounds";

        public BackgroundStore(Scheduler scheduler, Storage storage)
            : base(scheduler, storage)
        {
        }
    }
}
