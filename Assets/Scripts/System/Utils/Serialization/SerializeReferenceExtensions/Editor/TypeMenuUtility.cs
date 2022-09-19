using System;
using System.Collections.Generic;
using System.Linq;

namespace NavySpade.Modules.Utils.Serialization.SerializeReferenceExtensions.Editor
{
    public static class TypeMenuUtility
    {
        public const string NullDisplayName = "<null>";

        public static AddTypeMenuAttribute GetAttribute(Type type)
        {
            return Attribute.GetCustomAttribute(type, typeof(AddTypeMenuAttribute)) as AddTypeMenuAttribute;
        }

        public static string[] GetSplitTypePath(Type type)
        {
            var typeMenu = GetAttribute(type);
            if (typeMenu != null)
            {
                return typeMenu.GetSplitMenuName();
            }

            var splitIndex = type.FullName.LastIndexOf('.');
            if (splitIndex >= 0)
            {
                return new string[]
                    { type.FullName.Substring(0, splitIndex), type.FullName.Substring(splitIndex + 1) };
            }

            return new string[] { type.Name };
        }

        public static IEnumerable<Type> OrderByType(this IEnumerable<Type> source)
        {
            return source.OrderBy(type =>
            {
                if (type == null)
                {
                    return -999;
                }

                return GetAttribute(type)?.Order ?? 0;
            });
        }
    }
}