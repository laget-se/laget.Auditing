using System;

namespace laget.Auditing.Sinks.Elasticsearch.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IndexAttribute : Attribute
    {
        public virtual string IndexName { get; }
        public virtual string IndexFormat { get; }

        public IndexAttribute(string indexName, string indexFormat = "yyyy.MM.dd")
        {
            IndexName = indexName;
            IndexFormat = indexFormat;
        }
    }
}
