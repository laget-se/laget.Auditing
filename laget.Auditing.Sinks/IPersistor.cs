namespace laget.Auditing.Sinks
{
    public interface IPersistor
    {
        public void Persist(string name, object @object);
    }
}
