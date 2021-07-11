using System;
using UnityEngine;

namespace Arugula.Extensions
{
    /// <summary>
    /// Makes a string field a tag selection field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TagAttribute : PropertyAttribute
    {
    }
}
