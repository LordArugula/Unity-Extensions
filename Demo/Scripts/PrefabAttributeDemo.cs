using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class PrefabAttributeDemo : MonoBehaviour
    {
        [Prefab]
        public GameObject player;

        [Prefab]
        public Rigidbody projectile;
    }
}