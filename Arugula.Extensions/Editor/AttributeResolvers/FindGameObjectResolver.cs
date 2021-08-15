using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    public class FindGameObjectResolver : IReferenceResolver
    {
        public System.Type AttributeType => typeof(FindGameObjectAttribute);

        public Object FindObject(FieldInfo fieldInfo, Object owner)
        {
            if (IsAsset(owner))
            {
                Debug.LogWarning($"{fieldInfo.DeclaringType}.{fieldInfo.Name} is an asset and should not reference scene objects.", owner);
                return null;
            }

            FindGameObjectAttribute attr = fieldInfo.GetCustomAttribute<FindGameObjectAttribute>(true);
            string tag = attr.Tag;
            string name = attr.Name;

            Object obj = FindGameObject(fieldInfo, tag, name);
            if (obj != null)
            {
                return obj;
            }

            if (attr.CreateInstance)
            {
                return InstantiateObject(
                    fieldInfo,
                    tag ?? "Untagged",
                    name ?? fieldInfo.FieldType.Name);
            }

            return null;
        }

        private Object InstantiateObject(FieldInfo fieldInfo, string tag, string name)
        {
            GameObject go = new GameObject(name);
            go.tag = tag;
            if (TypeUtils.IsComponent(fieldInfo.FieldType))
            {
                return go.AddComponent(fieldInfo.FieldType);
            }
            return go;
        }

        private Object FindGameObject(FieldInfo fieldInfo, string tag, string name)
        {
            Object[] objects = GameObject.FindObjectsOfType(fieldInfo.FieldType, true);
            for (int i = 0; i < objects.Length; i++)
            {
                GameObject gameObject;
                if (TypeUtils.IsComponent(fieldInfo.FieldType))
                {
                    gameObject = (objects[i] as Component).gameObject;
                }
                else
                {
                    gameObject = objects[i] as GameObject;
                }

                if (!string.IsNullOrEmpty(tag)
                    && !gameObject.CompareTag(tag))
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(name)
                    && !gameObject.name.Equals(name, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                if (TypeUtils.IsComponent(fieldInfo.FieldType))
                {
                    return objects[i];
                }
                return gameObject;
            }
            return null;
        }

        private bool IsAsset(Object obj)
        {
            return EditorUtility.IsPersistent(obj);
        }

        public bool ValidateAttributeUsage(FieldInfo fieldInfo)
        {
            return TypeUtils.IsComponentOrGameObject(fieldInfo.FieldType);
        }
    }
}