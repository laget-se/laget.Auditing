namespace laget.Auditing.Sinks
{
    public interface IPersistor<T>
    {
        public void Persist(string name, T message);
    }
}
