using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Arugula.Extensions.Editor
{
    internal static class ReferenceResolverCache
    {
        private static List<ResolverFieldInfos> fieldInfoMap
            = new List<ResolverFieldInfos>();

        private static bool valid = false;

        [UnityEditor.Callbacks.DidReloadScripts]
        public static void Reload()
        {
            fieldInfoMap.Clear();
            GetResolverTypes();
            valid = true;
        }

        private static void GetResolverTypes()
        {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(IReferenceResolver));

            for (int i = 0; i < types.Count; i++)
            {
                if (!(types[i].IsAbstract || types[i].IsInterface))
                {
                    IReferenceResolver resolver = Activator.CreateInstance(types[i]) as IReferenceResolver;
                    fieldInfoMap.Add(new ResolverFieldInfos(resolver, GetFieldInfos(resolver)));
                }
            }
        }

        private static List<FieldInfo> GetFieldInfos(IReferenceResolver resolver)
        {
            TypeCache.FieldInfoCollection fieldInfoCollection
                = TypeCache.GetFieldsWithAttribute(resolver.AttributeType);

            List<FieldInfo> fieldInfos = new List<FieldInfo>(fieldInfoCollection.Count);
            for (int i = 0; i < fieldInfoCollection.Count; i++)
            {
                if (resolver.ValidateAttributeUsage(fieldInfoCollection[i]))
                {
                    fieldInfos.Add(fieldInfoCollection[i]);
                }
            }
            return fieldInfos;
        }

        public static ReadOnlyCollection<ResolverFieldInfos> GetFieldInfo()
        {
            if (!valid)
            {
                Reload();
            }
            return fieldInfoMap.AsReadOnly();
        }
    }

    internal struct ResolverFieldInfos
    {
        public IReferenceResolver resolver;
        public List<FieldInfo> fieldInfos;

        public ResolverFieldInfos(IReferenceResolver resolver, List<FieldInfo> fieldInfos)
        {
            this.resolver = resolver;
            this.fieldInfos = fieldInfos;
        }
    }
}