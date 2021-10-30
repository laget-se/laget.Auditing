using System;
using laget.Auditing.Sinks.Elasticsearch.Attributes;
using Nest;
using Newtonsoft.Json;

namespace laget.Auditing.Sinks.Elasticsearch.Models
{
    [Index]
    public class Message
    {
        [JsonProperty("id"), Text(Name = "sourceId")]
        public string SourceId { get; set; }
        [Text(Name = "action")]
        public string Action { get; set; }
        [Number(NumberType.Integer, Name = "clubId")]
        public int ClubId { get; set; }
        [Number(NumberType.Integer, Name = "siteId")]
        public int SiteId { get; set; }
        [Text(Name = "name")]
        public string Name { get; set; }
        [Text(Name = "by")]
        public object By
        {
            get => _by;
            set
            {
                _by = value;
                _by = JsonConvert.SerializeObject(By);
            }
        }
        private object _by;
        [Text(Name = "description")]
        public string Description { get; set; }
        [Text(Name = "system")]
        public string System { get; set; }
        [Text(Name = "entity")]
        public object Entity
        {
            get => _entity;
            set
            {
                _entity = value;
                _entity = JsonConvert.SerializeObject(Entity);
            }
        }
        private object _entity;
        [Text(Name = "reference")]
        public object Reference
        {
            get => _reference;
            set
            {
                _reference = value;
                _reference = JsonConvert.SerializeObject(Reference);
            }
        }
        private object _reference;

        [Date(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }
        [Date(Name = "persistedAt")]
        public DateTime PersistedAt { get; set; } = DateTime.Now;
    }
}
