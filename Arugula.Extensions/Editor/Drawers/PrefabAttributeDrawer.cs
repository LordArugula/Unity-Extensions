using System;
using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(PrefabAttribute))]
    public class PrefabAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                Rect rect = EditorGUI.PrefixLabel(position, label);
                property.objectReferenceValue
                    = EditorGUI.ObjectField(rect, property.objectReferenceValue, TypeUtils.GetElementType(fieldInfo), false);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
