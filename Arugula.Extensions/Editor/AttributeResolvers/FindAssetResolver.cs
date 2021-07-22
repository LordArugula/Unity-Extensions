using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    public class FindAssetResolver : IReferenceResolver
    {
        public System.Type AttributeType => typeof(FindAssetAttribute);

        public Object FindObject(FieldInfo fieldInfo, Object owner)
        {
            FindAssetAttribute attr = fieldInfo.GetCustomAttribute<FindAssetAttribute>(true);

            Object asset = FindAsset(fieldInfo, attr);
            if (asset != null)
            {
                return asset;
            }

            if (attr.CreateAsset)
            {
                return CreateAsset(fieldInfo, attr);
            }

            return null;
        }

        private Object CreateAsset(FieldInfo fieldInfo, FindAssetAttribute attr)
        {
            if (fieldInfo.FieldType.IsAbstract)
            {
                return null;
            }

            string name = attr.Name ?? fieldInfo.FieldType.Name;
            if (TypeUtils.IsComponentOrGameObject(fieldInfo.FieldType))
            {
                return CreatePrefabAsset(fieldInfo, name);
            }

            if (TypeUtils.IsScriptableObject(fieldInfo.FieldType))
            {
                return CreateScriptableObject(fieldInfo, name);
            }

            return null;
        }

        private static ScriptableObject CreateScriptableObject(FieldInfo fieldInfo, string name)
        {
            ScriptableObject so = ScriptableObject.CreateInstance(fieldInfo.FieldType);
            AssetDatabase.CreateAsset(so, $"Assets/{name}.asset");
            return so;
        }

        private Object CreatePrefabAsset(FieldInfo fieldInfo, string name)
        {
            GameObject obj = new GameObject(name);
            if (TypeUtils.IsComponent(fieldInfo.FieldType))
            {
                obj.AddComponent(fieldInfo.FieldType);
            }
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(obj, $"Assets/{name}.prefab", out bool success);
            GameObject.DestroyImmediate(obj, false);
            if (success)
            {
                if (TypeUtils.IsComponent(fieldInfo.FieldType))
                {
                    return prefab.GetComponent(fieldInfo.FieldType);
                }
                return prefab;
            }
            return null;
        }

        private Object FindAsset(FieldInfo fieldInfo, FindAssetAttribute attr)
        {
            string filter = $"t:{GetTypeFilter(fieldInfo.FieldType)} {attr.Name}";
            string[] guids = AssetDatabase.FindAssets(filter);

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                Object asset = AssetDatabase.LoadAssetAtPath<Object>(path);

                if (TypeUtils.IsComponent(fieldInfo.FieldType))
                {
                    Component component = (asset as GameObject).GetComponent(fieldInfo.FieldType);
                    if (component != null)
                    {
                        return component;
                    }
                    continue;
                }
                return asset;
            }
            return null;
        }

        private string GetTypeFilter(System.Type type)
        {
            if (TypeUtils.IsComponentOrGameObject(type))
            {
                return "Prefab";
            }
            return type.Name;
        }

        public bool ValidateAttributeUsage(FieldInfo fieldInfo)
        {
            return TypeUtils.IsUObject(fieldInfo.FieldType);
        }
    }
}