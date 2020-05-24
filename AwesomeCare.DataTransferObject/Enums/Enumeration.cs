using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AwesomeCare.DataTransferObject.Enums
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }
        public int Id { get; private set; }

        public Enumeration(int id,string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(t => t.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            var otherObj = obj as Enumeration;
            
            if (otherObj == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherObj.Id);

            return typeMatches && valueMatches;
        }
        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);
        
    }
}
