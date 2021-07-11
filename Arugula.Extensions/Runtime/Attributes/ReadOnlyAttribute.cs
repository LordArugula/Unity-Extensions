using System;
using UnityEngine;

namespace Arugula.Extensions
{
    /// <summary>
    /// Prevents editing fields via the Inspector during Play Mode, Edit Mode, or both.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
        public bool PlayMode { get; }

        public bool EditMode { get; }

        public ReadOnlyAttribute()
        {
            PlayMode = true;
            EditMode = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playMode">Prevents editing during Play Mode if set to true.</param>
        /// <param name="editMode">Prevents editing during Edit Mode if set to true.</param>
        public ReadOnlyAttribute(bool playMode = false, bool editMode = false)
        {
            PlayMode = playMode;
            EditMode = editMode;
        }
    }
}
