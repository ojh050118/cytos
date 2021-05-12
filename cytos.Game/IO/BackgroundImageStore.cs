using System.Collections.Generic;
using System.IO;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu.Framework.Threading;

namespace cytos.Game.IO
{
    public class BackgroundImageStore : BackgroundStore<Texture>
    {
        protected override IEnumerable<string> Filters => new[] { "*.jpg", "*.jpeg", "*.png" };

        public BackgroundImageStore(Scheduler scheduler, Storage storage)
            : base(scheduler, storage)
        {
        }

        protected override Texture Load(Stream stream) => Texture.FromStream(stream);
    }
}
