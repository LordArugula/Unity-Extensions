using System;
using UnityEngine;

namespace Arugula.Extensions
{
    /// <summary>
    /// Makes integer or string fields a layer selection field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class LayerAttribute : PropertyAttribute
    {
    }
}
