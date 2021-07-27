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

                        if (IsReferenceNull(fieldInfo.GetValue(owner)))
                        {
                            UnityEngine.Object obj = resolver.FindObject(fieldInfo, owner);
                            if (obj != null)
                            {
                                AssignObject(fieldInfo, owner, obj);
                            }
                            else
                            {
                                Debug.LogWarning($"Could not assign <color=cyan>{fieldInfo.DeclaringType.Name}</color>.<color=yellow>{fieldInfo.Name}</color> on <color=cyan>{owner.name}</color>.", owner);
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
                PrefabUtility.RecordPrefabInstancePropertyModifications(owner);
            }
            else
            {
                EditorUtility.SetDirty(owner);
            }
            fieldInfo.SetValue(owner, obj);
        }

        private static bool IsReferenceNull(object value)
        {
            return value == null || value.Equals(null);
        }
    }
}