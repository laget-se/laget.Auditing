using System;

namespace laget.Auditing.Sinks.Elasticsearch.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IndexAttribute : Attribute
    {
        public virtual string IndexFormat { get; }

        public IndexAttribute(string indexFormat = "yyyy.MM.dd")
        {
            IndexFormat = indexFormat;
        }
    }
}
