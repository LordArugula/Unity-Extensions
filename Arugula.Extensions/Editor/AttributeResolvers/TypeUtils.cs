using System;
using UnityEngine;

namespace Arugula.Extensions.Editor
{
    public static class TypeUtils
    {
        public static bool IsUObject(Type type)
        {
            return type.IsSubclassOf(typeof(UnityEngine.Object));
        }

        public static bool IsComponent(Type type)
        {
            return type.IsSubclassOf(typeof(Component));
        }

        public static bool IsGameObject(Type type)
        {
            return type == typeof(GameObject);
        }

        public static bool IsComponentOrGameObject(Type type)
        {
            return IsComponent(type) || IsGameObject(type);
        }

        public static bool IsScriptableObject(Type type)
        {
            return type.IsSubclassOf(typeof(ScriptableObject));
        }
    }
}