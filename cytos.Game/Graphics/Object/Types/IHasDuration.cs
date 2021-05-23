using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cytos.Game.Graphics.Object.Types
{
    public interface IHasDuration
    {
        double EndTime { get; }

        double Duration { get; set; }
    }
}
