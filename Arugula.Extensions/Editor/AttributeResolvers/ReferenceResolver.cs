using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    internal static class ReferenceResolver
    {
        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
            ResolveReferenceAttributes();
        }

        private static void OnHierarchyChanged()
        {
            ResolveReferenceAttributes();
        }

        public static void ResolveReferenceAttributes()
        {
            ReadOnlyCollection<ResolverFieldInfos> fieldInfoMap
                = ReferenceResolverCache.GetFieldInfo();
            for (int i = 0; i < fieldInfoMap.Count; i++)
            {
                IReferenceResolver resolver = fieldInfoMap[i].resolver;
                List<FieldInfo> fieldInfos = fieldInfoMap[i].fieldInfos;
                for (int f = 0; f < fieldInfos.Count; f++)
                {
                    FieldInfo fieldInfo = fieldInfos[f];
                    UnityEngine.Object[] objectsWithField
                        = Resources.FindObjectsOfTypeAll(fieldInfo.DeclaringType);
                    for (int o = 0; o < objectsWithField.Length; o++)
                    {
                        UnityEngine.Object owner = objectsWithField[o];

                        if (IsFieldReferenceNull(fieldInfo.GetValue(owner)))
                        {
                            UnityEngine.Object obj = resolver.FindObject(fieldInfo, owner);
                            if (obj != null)
                            {
                                AssignObject(fieldInfo, owner, obj);
                            }
                            else
                            {
                                Debug.LogWarning($"Could not assign {fieldInfo.DeclaringType.Name}.{fieldInfo.Name} on {owner.name}.", owner);
                            }
                        }
                    }
                }
            }
        }

        private static void AssignObject(FieldInfo fieldInfo, UnityEngine.Object owner, UnityEngine.Object obj)
        {
            if (PrefabUtility.IsPartOfPrefabAsset(owner))
            {
                PrefabUtility.RecordPrefabInstancePropertyModifications(PrefabUtility.GetNearestPrefabInstanceRoot(owner));
            }
            else
            {
                EditorUtility.SetDirty(owner);
            }
            fieldInfo.SetValue(owner, obj);
        }

        private static bool IsFieldReferenceNull(object value)
        {
            return value == null || value.Equals(null);
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

        private static List<IReferenceResolver> GetResolvers()
        {
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(IReferenceResolver));
            List<IReferenceResolver> validResolverTypes = new List<IReferenceResolver>(types.Count);

            for (int i = 0; i < types.Count; i++)
            {
                if (!(types[i].IsAbstract || types[i].IsInterface))
                {
                    validResolverTypes.Add(Activator.CreateInstance(types[i]) as IReferenceResolver);
                }
            }

            return validResolverTypes;
        }
    }
}