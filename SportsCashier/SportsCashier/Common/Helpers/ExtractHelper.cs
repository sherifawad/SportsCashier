using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsCashier.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
    public sealed class ExtractNameAttribute : Attribute
    { }
    public static class ExtractHelper
    {

        public static IEnumerable<string> IterateProps(Type baseType)
        {
            return IteratePropsInner(baseType, baseType.Name);
        }

        private static IEnumerable<string> IteratePropsInner(Type baseType, string baseName)
        {
            var props = baseType.GetProperties();

            foreach (var property in props)
            {
                var name = property.Name;
                var type = ListArgumentOrSelf(property.PropertyType);
                if (IsMarked(type))
                    foreach (var info in IteratePropsInner(type, name))
                        yield return string.Format("{0}.{1}", baseName, info);
                else
                    yield return string.Format("{0}.{1}", baseName, property.Name);
            }
        }

        private static bool IsMarked(Type type)
        {
            return type.GetCustomAttributes(typeof(ExtractNameAttribute), true).Any();
        }


        private static Type ListArgumentOrSelf(Type type)
        {
            if (!type.IsGenericType)
                return type;
            if (type.GetGenericTypeDefinition() != typeof(List<>))
                throw new Exception("Only List<T> are allowed");
            return type.GetGenericArguments()[0];
        }
    }

    [ExtractName]
    public class Container
    {
        public string Name { get; set; }
        //public List<ValidatableObject<string>> Addresses { get; set; }
    }

}
