using System;

namespace laget.Auditing.Sinks.Elasticsearch
{
    public  class Persistor : IPersistor
    {
        public void Persist(string indexName, object @object)
        {
            throw new NotImplementedException();
        }
    }
}
