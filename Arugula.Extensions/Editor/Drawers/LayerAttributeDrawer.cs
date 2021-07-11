using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                int layerIndex = ClampLayerIndex(property.intValue);
                property.intValue = EditorGUI.LayerField(position, label, layerIndex);
            }
            else if (property.propertyType == SerializedPropertyType.String)
            {
                int layerIndex =
                    string.IsNullOrEmpty(property.stringValue)
                    ? 0
                    : LayerMask.NameToLayer(property.stringValue);
                layerIndex = ClampLayerIndex(layerIndex);
                layerIndex = EditorGUI.LayerField(position, label, layerIndex);
                string layerName = LayerMask.LayerToName(layerIndex);
                property.stringValue = layerName;
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        private int ClampLayerIndex(int layerIndex)
        {
            return Mathf.Clamp(layerIndex, 0, UnityEditorInternal.InternalEditorUtility.layers.Length - 1);
        }
    }
}