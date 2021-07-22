using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    public class GetComponentResolver : IReferenceResolver
    {
        public System.Type AttributeType => typeof(GetComponentAttribute);

        public Object FindObject(FieldInfo fieldInfo, Object owner)
        {
            Component ownerComponent = owner as Component;
            if (ownerComponent == null)
            {
                return null;
            }

            return GetComponent(fieldInfo, ownerComponent);
        }

        private Component GetComponent(FieldInfo fieldInfo, Component owner)
        {
            GetComponentAttribute attr = fieldInfo.GetCustomAttribute<GetComponentAttribute>();

            Component component = attr.IncludeChildren
                ? owner.GetComponentInChildren(fieldInfo.FieldType, attr.IncludeInactive)
                : owner.GetComponent(fieldInfo.FieldType);

            if (component == null && attr.AddComponent)
            {
                component = owner.gameObject.AddComponent(fieldInfo.FieldType);
            }

            return component;
        }

        public bool ValidateAttributeUsage(FieldInfo fieldInfo)
        {
            return TypeUtils.IsComponent(fieldInfo.FieldType)
                && TypeUtils.IsComponent(fieldInfo.DeclaringType)
                && !fieldInfo.FieldType.IsAbstract;
        }
    }
}