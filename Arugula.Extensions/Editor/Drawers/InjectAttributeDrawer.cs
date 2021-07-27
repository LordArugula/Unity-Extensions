using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(InjectAttribute))]
    public class InjectAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsManagedReference(property))
            {
                Type fieldType = GetType(property.managedReferenceFieldTypename);
                List<Type> injectableTypes = GetInjectableTypes(fieldType);

                int selectedIndex = GetSelectedIndex(injectableTypes, property);
                int newIndex = DrawTypesDropdown(position, injectableTypes, selectedIndex);

                if (newIndex != selectedIndex)
                {
                    Inject(property, CreateInstance(injectableTypes[newIndex]));
                }
            }
            EditorGUI.PropertyField(position, property, label, true);
        }

        private int GetSelectedIndex(List<Type> injectableTypes, SerializedProperty property)
        {
            if (string.IsNullOrEmpty(property.managedReferenceFullTypename))
            {
                return 0;
            }

            Type objectType = GetType(property.managedReferenceFullTypename);
            if (objectType != null)
            {
                return injectableTypes.IndexOf(objectType);
            }
            return 0;
        }

        private Type GetType(string typeString)
        {
            string[] typeName = typeString.Split(' ');
            return Type.GetType($"{typeName[1]}, {typeName[0]}");
        }

        private int DrawTypesDropdown(Rect position, List<Type> injectableTypes, int selectedIndex)
        {
            float labelWidth = EditorGUIUtility.labelWidth;
            float lineHeight = EditorGUIUtility.singleLineHeight;
            Rect popupPosition = new Rect(position.x + labelWidth, position.y, position.width - labelWidth, lineHeight);

            GUIContent[] displayedOptions = GenerateOptionsContent(injectableTypes);
            return EditorGUI.Popup(popupPosition, selectedIndex, displayedOptions);
        }

        private bool IsManagedReference(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.ManagedReference;
        }

        private object CreateInstance(Type type)
        {
            if (type == null)
            {
                return null;
            }
            return Activator.CreateInstance(type);
        }

        private void Inject(SerializedProperty property, object injectedObject)
        {
            property.managedReferenceValue = injectedObject;
        }

        private GUIContent[] GenerateOptionsContent(List<Type> injectableTypes)
        {
            GUIContent[] options = new GUIContent[injectableTypes.Count];
            options[0] = new GUIContent("None");
            for (int i = 1; i < injectableTypes.Count; i++)
            {
                options[i] = new GUIContent(ObjectNames.NicifyVariableName(injectableTypes[i].Name));
            }
            return options;
        }

        private List<Type> GetInjectableTypes(Type fieldType)
        {
            List<Type> types = new List<Type>();
            types.Add(null);

            TypeCache.TypeCollection typeCollection = TypeCache.GetTypesDerivedFrom(fieldType);
            for (int i = 0; i < typeCollection.Count; i++)
            {
                if (IsTypeInjectable(typeCollection[i]))
                {
                    types.Add(typeCollection[i]);
                }
            }

            return types;
        }

        private bool IsTypeInjectable(Type type)
        {
            return !(type.IsAbstract || type.IsInterface);
        }
    }
}
