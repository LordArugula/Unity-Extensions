using System;

namespace Arugula.Extensions
{
    /// <summary>
    /// Forces a reference type field to be assigned before entering Play Mode.
    /// <para>
    /// This is meant for fields that should be assigned in the Inspector
    /// during Edit Mode, rather than at run-time.
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class RequireReferenceAttribute : Attribute
    {

    }
}
