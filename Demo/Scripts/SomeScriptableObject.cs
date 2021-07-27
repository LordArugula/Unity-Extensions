using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class SomeScriptableObject : ScriptableObject
    {
        [FindAsset]
        public GameObject prefab;

        [RequireReference]
        public GameObject required;
    }
}