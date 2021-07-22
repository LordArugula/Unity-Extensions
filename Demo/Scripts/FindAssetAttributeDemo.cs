using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class FindAssetAttributeDemo : MonoBehaviour
    {
        [FindAsset(Name = "Player")]
        public GameObject player;

        [FindAsset(Name = "Bullet", CreateAsset = true)]
        public Rigidbody bullet;

        [FindAsset]
        public GameManager prefab;

        [FindAsset(CreateAsset = true)]
        public SomeScriptableObject scriptableObject;
    }
}
