using System;
using UnityEngine;

namespace Arugula.Extensions
{
    /// <summary>
    /// Makes an integer field a scene selection field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SceneAttribute : PropertyAttribute
    {
    }
}
