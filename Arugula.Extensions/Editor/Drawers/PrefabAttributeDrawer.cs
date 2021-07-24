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
                Rect prefabFieldRect = EditorGUI.PrefixLabel(position, label);
                property.objectReferenceValue = EditorGUI.ObjectField(prefabFieldRect, property.objectReferenceValue, fieldInfo.FieldType, false);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
