using System;

namespace Arugula.Extensions
{
    /// <summary>
    /// Finds a GameObject in the scene and assigns it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class FindGameObjectAttribute : Attribute
    {
        /// <summary>
        /// The name of the GameObject.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the GameObject
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Whether to create a GameObject if one is not found.
        /// </summary>
        public bool CreateInstance { get; set; }
    }
}