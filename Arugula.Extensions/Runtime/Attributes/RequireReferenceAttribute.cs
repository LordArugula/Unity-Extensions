using System;

namespace Arugula.Extensions
{
    /// <summary>
    /// Forces a reference type field to be assigned before entering Play Mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class RequireReferenceAttribute : Attribute
    {

    }
}
