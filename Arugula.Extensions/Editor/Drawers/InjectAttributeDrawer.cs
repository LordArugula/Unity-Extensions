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
            EditorGUI.BeginProperty(position, label, property);

            if (IsPropertyTypeManagedReference(property))
            {
                Type fieldType = GetFieldType();
                List<Type> injectableTypes = GetInjectableTypes(fieldType);

                object injectedObject = GetInjectedObject(property);
                int selectedIndex = 0;
                if (injectedObject != null)
                {
                    selectedIndex = injectableTypes.IndexOf(injectedObject.GetType());
                }

                float labelWidth = EditorGUIUtility.labelWidth;
                float lineHeight = EditorGUIUtility.singleLineHeight;
                Rect popupPosition = new Rect(position.x + labelWidth, position.y, position.width - labelWidth, lineHeight);

                int index = EditorGUI.Popup(popupPosition, selectedIndex, GenerateInjectableOptionsContent(injectableTypes));

                if (index != selectedIndex)
                {
                    Inject(property, CreateInstance(injectableTypes[index]));
                }
            }
            EditorGUI.PropertyField(position, property, label, true);

            EditorGUI.EndProperty();
        }

        private Type GetFieldType()
        {
            if (typeof(System.Collections.IList).IsAssignableFrom(fieldInfo.FieldType))
            {
                if (fieldInfo.FieldType.IsArray)
                {
                    return fieldInfo.FieldType.GetElementType();
                }
                else if (fieldInfo.FieldType.GenericTypeArguments.Length == 1)
                {
                    return fieldInfo.FieldType.GenericTypeArguments[0];
                }
            }
            return fieldInfo.FieldType;
        }

        private bool IsPropertyTypeManagedReference(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.ManagedReference;
        }

        private object GetInjectedObject(SerializedProperty property)
        {
            object value = fieldInfo.GetValue(property.serializedObject.targetObject);
            if (typeof(System.Collections.IList).IsAssignableFrom(fieldInfo.FieldType))
            {
                if (fieldInfo.FieldType.IsArray || fieldInfo.FieldType.GenericTypeArguments.Length == 1)
                {
                    System.Collections.IList list = (System.Collections.IList)value;

                    int start = property.propertyPath.LastIndexOf('[') + 1;
                    string arrayIndex = property.propertyPath.Substring(start, property.propertyPath.Length - start - 1);
                    value = list[int.Parse(arrayIndex)];
                }
            }

            return value;
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
            property.serializedObject.ApplyModifiedProperties();
        }

        private GUIContent[] GenerateInjectableOptionsContent(List<Type> injectableTypes)
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
