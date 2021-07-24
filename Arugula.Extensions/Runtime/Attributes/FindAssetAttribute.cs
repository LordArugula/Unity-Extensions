using System;

namespace Arugula.Extensions
{
    /// <summary>
    /// Finds an asset and assigns it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class FindAssetAttribute : Attribute
    {
        /// <summary>
        /// The name of the asset.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether to create a default asset if one is not found.
        /// <para>Only applies to ScriptableObject or Component fields.</para>
        /// </summary>
        public bool CreateAsset { get; set; }
    }
}
