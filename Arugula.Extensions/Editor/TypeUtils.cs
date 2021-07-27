using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    public static class TypeUtils
    {
        /// <summary>
        /// Checks if a field is a list or array type.
        /// </summary>
        /// <param name="fieldInfo">The field meta data.</param>
        /// <returns></returns>
        public static bool IsListOrArray(FieldInfo fieldInfo)
        {
            return (typeof(IList).IsAssignableFrom(fieldInfo.FieldType)
                && fieldInfo.FieldType.GenericTypeArguments.Length == 1)
                || fieldInfo.FieldType.IsArray;
        }

        /// <summary>
        /// Gets the element type of a list or array from the field info.
        /// </summary>
        /// <param name="fieldInfo">The field meta data.</param>
        /// <returns>
        /// Returns the element type of a list or array from the field info.
        /// If the field type is not an array, returns the field type.
        /// </returns>
        public static Type GetElementType(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                return fieldInfo.FieldType.GetElementType();
            }
            else if (typeof(IList).IsAssignableFrom(fieldInfo.FieldType)
                && fieldInfo.FieldType.GenericTypeArguments.Length == 1)
            {
                return fieldInfo.FieldType.GenericTypeArguments[0];
            }
            return fieldInfo.FieldType;
        }

        /// <summary>
        /// Checks if the type is a subclass of <seealso cref="UnityEngine.Object" />.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsUObject(Type type)
        {
            return type.IsSubclassOf(typeof(UnityEngine.Object));
        }

        /// <summary>
        /// Checks if the type is a subclass of <seealso cref="Component" />.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsComponent(Type type)
        {
            return type.IsSubclassOf(typeof(Component));
        }

        /// <summary>
        /// Checks if the type is typeof(<seealso cref="GameObject" />).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGameObject(Type type)
        {
            return type == typeof(GameObject);
        }

        /// <summary>
        /// Checks if the type is a subclass of <seealso cref="Component" />
        /// or is typeof(<seealso cref="GameObject" />).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsComponentOrGameObject(Type type)
        {
            return IsComponent(type) || IsGameObject(type);
        }

        /// <summary>
        /// Checks if the type is a subclass of <seealso cref="ScriptableObject" />.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsScriptableObject(Type type)
        {
            return type.IsSubclassOf(typeof(ScriptableObject));
        }
    }
}