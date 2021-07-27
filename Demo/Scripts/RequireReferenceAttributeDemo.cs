using System.Collections.Generic;
using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class RequireReferenceAttributeDemo : MonoBehaviour
    {
        [RequireReference, SerializeReference]
        private Animator animator;

        [RequireReference, Prefab]
        public List<GameObject> prefabs;

        [RequireReference, SerializeReference, Inject]
        private IService service;
    }
}
