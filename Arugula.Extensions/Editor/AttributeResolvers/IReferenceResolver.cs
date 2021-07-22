using System;
using System.Collections.Generic;
using System.Reflection;

namespace Arugula.Extensions.Editor
{
    /// <summary>
    /// Implement this interface to register an attribute.
    /// </summary>
    public interface IReferenceResolver
    {
        /// <summary>
        /// Checks that the attribute is applied to a valid field and in a valid class.
        /// </summary>
        /// <param name="fieldInfo">The field metadata.</param>
        /// <returns>Returns true if the attribute is used in a valid way.</returns>
        bool ValidateAttributeUsage(FieldInfo fieldInfo);

        /// <summary>
        /// Finds an Object that matches the attribute and field info.
        /// </summary>
        /// <param name="fieldInfo">The field metadata.</param>
        /// <param name="owner">The Object that owns the field.</param>
        /// <returns></returns>
        UnityEngine.Object FindObject(FieldInfo fieldInfo, UnityEngine.Object owner);

        /// <summary>
        /// The corresponding attribute type.
        /// </summary>
        Type AttributeType { get; }
    }
}