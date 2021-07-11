using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReadOnlyAttribute attr = attribute as ReadOnlyAttribute;
            bool disabled = (EditorApplication.isPlayingOrWillChangePlaymode && attr.PlayMode)
                || (EditorApplication.isPlayingOrWillChangePlaymode ^ attr.EditMode);

            using (_ = new EditorGUI.DisabledScope(disabled))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
    }
}
