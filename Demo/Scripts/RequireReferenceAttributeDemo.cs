using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class RequireReferenceAttributeDemo : MonoBehaviour
    {
        [RequireReference]
        public Animator animator;

        [RequireReference, Prefab]
        public GameObject prefab;

        [RequireReference, SerializeReference, Inject]
        public IService list;
    }
}
