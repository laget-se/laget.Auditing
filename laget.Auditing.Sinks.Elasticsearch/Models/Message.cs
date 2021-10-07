using System;

namespace laget.Auditing.Sinks.Elasticsearch.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SourceId { get; set; }
        public int ClubId { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public object By { get; set; }
        public string Description { get; set; }
        public object Entity { get; set; }
        public object Reference { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime PersistedAt { get; set; } = DateTime.Now;
    }
}
