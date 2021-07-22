using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class SceneAttributeDemo : MonoBehaviour
    {
        [Scene]
        public int mainScene;

        [Scene]
        public string otherScene;
    }
}