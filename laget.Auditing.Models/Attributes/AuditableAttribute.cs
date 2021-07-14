using System;

namespace laget.Auditing.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]

    public class AuditableAttribute : Attribute
    {
        public string Name { get; set; }

        public AuditableAttribute(string name)
        {
            Name = name;
        }
    }
}
