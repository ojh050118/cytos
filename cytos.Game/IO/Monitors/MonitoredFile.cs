namespace cytos.Game.IO.Monitors
{
    public class MonitoredFile
    {
        public string Name { get; }

        public MonitoredFile(string name)
        {
            Name = name;
        }
    }

    public class MonitoredFile<T> : MonitoredFile
        where T : class
    {
        public T Data { get; }

        public MonitoredFile(string name, T data)
            : base(name)
        {
            Data = data;
        }

        public override string ToString() => Name;
    }
}
