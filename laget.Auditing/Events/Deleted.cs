﻿using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Deleted : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Deleted);

        public Deleted(object entity)
            : base(entity)
        {
        }
    }
}
