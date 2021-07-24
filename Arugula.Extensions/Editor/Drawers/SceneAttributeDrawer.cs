using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Integer
            || property.propertyType == SerializedPropertyType.String)
            {
                DoSceneField(position, property, label);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        private void DoSceneField(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect dropDownRect = EditorGUI.PrefixLabel(position, label);
            int sceneIndex = GetSceneIndex(property);
            if (!IsValidSceneIndex(sceneIndex))
            {
                if (IsIntegerProperty(property))
                {
                    property.intValue = 0;
                }
                sceneIndex = 0;
            }
            string currentSceneName = GetSceneNameAtIndex(sceneIndex);
            if (EditorGUI.DropdownButton(dropDownRect, new GUIContent(currentSceneName), FocusType.Keyboard))
            {
                DoSceneListDropdown(dropDownRect, property, sceneIndex);
            }
        }

        private bool IsValidSceneIndex(int sceneIndex)
        {
            return sceneIndex >= 0 && sceneIndex < EditorBuildSettings.scenes.Length;
        }

        private int GetSceneIndex(SerializedProperty property)
        {
            return IsIntegerProperty(property)
                ? property.intValue
                : GetSceneIndexFromName(property.stringValue);
        }

        private int GetSceneIndexFromName(string sceneName)
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            for (int i = 0; i < scenes.Length; i++)
            {
                if (GetSceneNameAtIndex(i) == sceneName)
                {
                    return i;
                }
            }
            return -1;
        }

        private string GetSceneNameAtIndex(int sceneIndex)
        {
            if (EditorBuildSettings.scenes.Length == 0)
            {
                return "<No Scenes in Build Settings>";
            }

            return System.IO.Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[sceneIndex].path);
        }

        private void DoSceneListDropdown(Rect position, SerializedProperty property, int sceneIndex)
        {
            GenericMenu menu = new GenericMenu();

            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            for (int i = 0; i < scenes.Length; i++)
            {
                menu.AddItem(new GUIContent(GetSceneNameAtIndex(i)),
                    sceneIndex == i,
                    SetSceneIndex,
                    new SceneInfo
                    {
                        Property = property,
                        SceneIndex = i
                    });
            }

            if (scenes.Length > 0)
            {
                menu.AddSeparator("");
            }

            menu.AddItem(new GUIContent("Open Build Settings"), false, OpenBuildSettingsWindow);
            menu.DropDown(position);
        }

        private void OpenBuildSettingsWindow()
        {
            EditorWindow.GetWindow<BuildPlayerWindow>();
        }

        private void SetSceneIndex(object userData)
        {
            SceneInfo sceneInfo = (SceneInfo)userData;
            if (IsIntegerProperty(sceneInfo.Property))
            {
                sceneInfo.Property.intValue = sceneInfo.SceneIndex;
            }
            else
            {
                sceneInfo.Property.stringValue = GetSceneNameAtIndex(sceneInfo.SceneIndex);
            }
            sceneInfo.Property.serializedObject.ApplyModifiedProperties();
        }

        private bool IsIntegerProperty(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.Integer;
        }
    }

    public struct SceneInfo
    {
        public int SceneIndex { get; set; }
        public SerializedProperty Property { get; set; }
    }
}