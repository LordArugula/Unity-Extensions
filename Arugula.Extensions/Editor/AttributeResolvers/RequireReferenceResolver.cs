using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    public static class RequireReferenceResolver
    {
        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.ExitingEditMode)
            {
                ValidateRequireReferenceFields();
            }
        }

        private static bool anyFieldMissingReference = false;

        private static void ValidateRequireReferenceFields()
        {
            anyFieldMissingReference = false;
            Dictionary<Type, List<FieldInfo>> map = MapUEObjectsToFieldInfos();
            CheckMissingReferences(map);
            if (anyFieldMissingReference)
            {
                EditorApplication.ExitPlaymode();
            }
        }

        private static Dictionary<Type, List<FieldInfo>> MapUEObjectsToFieldInfos()
        {
            TypeCache.FieldInfoCollection fields = TypeCache.GetFieldsWithAttribute<RequireReferenceAttribute>();
            Dictionary<Type, List<FieldInfo>> fieldInfos = new Dictionary<Type, List<FieldInfo>>(fields.Count);
            for (int i = 0; i < fields.Count; i++)
            {
                FieldInfo fieldInfo = fields[i];
                if (TypeUtils.IsComponent(fieldInfo.DeclaringType)
                    || TypeUtils.IsScriptableObject(fieldInfo.DeclaringType))
                {
                    if (fieldInfos.ContainsKey(fieldInfo.DeclaringType))
                    {
                        fieldInfos[fieldInfo.DeclaringType].Add(fieldInfo);
                    }
                    else
                    {
                        fieldInfos.Add(fieldInfo.DeclaringType, new List<FieldInfo> { fieldInfo });
                    }
                }
            }

            return fieldInfos;
        }

        private static void CheckMissingReferences(Dictionary<Type, List<FieldInfo>> objectTypesMap)
        {
            foreach (KeyValuePair<Type, List<FieldInfo>> typeFieldsPair in objectTypesMap)
            {
                Type type = typeFieldsPair.Key;
                List<FieldInfo> fieldInfos = typeFieldsPair.Value;
                UnityEngine.Object[] objs = UnityEngine.Object.FindObjectsOfType(type, true);

                for (int i = 0; i < objs.Length; i++)
                {
                    UnityEngine.Object obj = objs[i];
                    for (int j = 0; j < fieldInfos.Count; j++)
                    {
                        CheckMissingReferences(obj, fieldInfos[j]);
                    }
                }
            }
        }

        private static void CheckMissingReferences(UnityEngine.Object obj, FieldInfo fieldInfo)
        {
            object value = fieldInfo.GetValue(obj);
            if (TypeUtils.IsListOrArray(fieldInfo))
            {
                IList list = (IList)value;
                for (int i = 0; i < list.Count; i++)
                {
                    object arrayValue = list[i];
                    if (arrayValue == null || arrayValue.Equals(null))
                    {
                        Debug.LogError($"<color=cyan>{obj.name}</color> must assign <color=cyan>{obj.GetType().Name}</color>.<color=yellow>{fieldInfo.Name}</color>[{i}].", obj);
                        anyFieldMissingReference = true;
                    }
                }
            }
            else if (value == null || value.Equals(null))
            {
                Debug.LogError($"<color=cyan>{obj.name}</color> must assign <color=yellow>{obj.GetType().Name}</color>.<color=yellow>{fieldInfo.Name}</color>.", obj);
                anyFieldMissingReference = true;
            }
        }
    }
}
