namespace cytos.Game.Graphics.Object.Types
{
    public interface IHasDuration
    {
        double EndTime { get; }

        double Duration { get; set; }
    }
}
