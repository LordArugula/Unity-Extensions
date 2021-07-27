using System;
using UnityEngine;

namespace Arugula.Extensions
{
    /// <summary>
    /// Allows injecting dependencies in the Unity Inspector to fields with the <seealso cref="SerializeReference" /> attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class InjectAttribute : PropertyAttribute
    {
    }
}
