namespace laget.Auditing.Sinks
{
    public interface IPersistor<T>
    {
        public bool Configured { get; }
        public void Persist(string name, T message);
    }
}
