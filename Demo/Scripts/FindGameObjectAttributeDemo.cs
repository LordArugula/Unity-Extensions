using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class FindGameObjectAttributeDemo : MonoBehaviour
    {
        [FindGameObject(Tag = "Player")]
        public GameObject player;

        [FindGameObject(Name = "Main Camera", Tag = "MainCamera")]
        public Camera mainCamera;

        [FindGameObject(CreateInstance = true, Name = "Game Manager")]
        public GameManager gameManager;
    }
}
