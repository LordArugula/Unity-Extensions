using System;
using UnityEngine;

namespace Arugula.Extensions
{
    /// <summary>
    /// Restricts a GameObject or Component field to only accept prefab assets.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class PrefabAttribute : PropertyAttribute
    {
    }
}