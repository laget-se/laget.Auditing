using System;
using System.Collections.Concurrent;

namespace laget.Auditing.Models.Constants
{
    public class Action
    {
        private static readonly ConcurrentDictionary<string, Action> Actions = new ConcurrentDictionary<string, Action>();

        public static readonly Action Added = new Action("Added");
        public static readonly Action Create = new Action("Created");
        public static readonly Action Delete = new Action("Deleted");
        public static readonly Action Information = new Action("Information");
        public static readonly Action Remove = new Action("Removed");
        public static readonly Action Update = new Action("Updated");

        public string Name { get; }

        private Action(string name)
        {
            Name = name;
            Actions.AddOrUpdate(Name, this, (k, v) => v);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Action lhs, Action rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return ReferenceEquals(rhs, null);
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Action lhs, Action rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Action))
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }
            var rhs = (Action)obj;
            if (Name == null)
            {
                return rhs.Name == null;
            }
            return Name.Equals(rhs.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
