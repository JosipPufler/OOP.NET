using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    //https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }
        public string Path { get; private set; }

        protected Enumeration(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public override string ToString() => Name + " " + Path;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Name.ToLower().Equals(otherValue.Name.ToLower()) && Path.ToLower().Equals(otherValue.Path.ToLower());

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Name.ToLower().CompareTo(((Enumeration)other).Name.ToLower());
    }
}
