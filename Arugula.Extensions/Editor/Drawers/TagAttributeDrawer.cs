using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            string tag = IsValidTag(property.stringValue) ? property.stringValue : GetUntaggedString();
            property.stringValue = EditorGUI.TagField(position, label, tag);
        }

        private string GetUntaggedString()
        {
            return UnityEditorInternal.InternalEditorUtility.tags[0];
        }

        private bool IsValidTag(string tag)
        {
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            for (int i = 0; i < tags.Length; i++)
            {
                if (tags[i] == tag)
                {
                    return true;
                }
            }
            return false;
        }
    }
}