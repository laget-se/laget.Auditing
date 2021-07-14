using System;

namespace laget.Auditing.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]

    public class AuditableAttribute : Attribute
    {
        public string Name { get; set; }

        public AuditableAttribute()
        {
        }

        public AuditableAttribute(string name)
        {
            Name = name;
        }
    }
}
