using System;
using UnityEngine;

namespace Arugula.Extensions
{
    /// <summary>
    /// Allows injecting dependencies in the Unity Inspector to fields with the <seealso name="SerializeReference" /> attribute.
    /// </summary>
    /// <remarks>
    /// SerializeReference does not serialize innflated types, specific specialization of a generic type, so Inject will not either.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class InjectAttribute : PropertyAttribute
    {
    }
}
