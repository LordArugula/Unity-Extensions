using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class GetComponentAttributeDemo : MonoBehaviour
    {
        [GetComponent(AddComponent = true)]
        public Rigidbody _rigidbody;

        [GetComponent(IncludeChildren = true)]
        public Animator _animator;
    }
}
