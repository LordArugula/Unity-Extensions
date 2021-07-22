using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class ReadOnlyAttributeDemo : MonoBehaviour
    {
        [ReadOnly]
        public GameObject readOnlyAlways;

        [ReadOnly(playMode: true)]
        public int readOnlyDuringPlayMode;

        [ReadOnly(editMode: true)]
        public float readOnlyDuringEditMode;
    }
}
