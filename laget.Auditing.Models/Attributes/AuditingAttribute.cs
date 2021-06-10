using System;

namespace laget.Auditing.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]

    public class AuditingAttribute : Attribute
    {
        public string Name { get; set; }

        public AuditingAttribute(string name)
        {
            Name = name;
        }
    }
}
