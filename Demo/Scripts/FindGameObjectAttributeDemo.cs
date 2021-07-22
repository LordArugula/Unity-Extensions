using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class FindGameObjectAttributeDemo : MonoBehaviour
    {
        [FindGameObject(Tag = "Player")]
        public GameObject player;

        [FindGameObject(Name = "MainCamera")]
        public Camera mainCamera;

        [FindGameObject(CreateInstance = true)]
        public GameManager gameManager;
    }
}
