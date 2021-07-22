using System;

namespace Arugula.Extensions
{
    /// <summary>
    /// Finds a Component on the GameObject or its children and assigns it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class GetComponentAttribute : Attribute
    {
        /// <summary>
        /// Whether child GameObjects are searched.
        /// </summary>
        public bool IncludeChildren { get; set; }

        /// <summary>
        /// Whether inactive child GameObjects are searched.
        /// </summary>
        public bool IncludeInactive { get; set; }

        /// <summary>
        /// Whether a component is added to the GameObject if a component is not found.
        /// </summary>
        public bool AddComponent { get; set; }
    }
}

