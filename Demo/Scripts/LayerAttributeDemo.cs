using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public class LayerAttributeDemo : MonoBehaviour
    {
        [Layer]
        public int groundLayer;

        [Layer]
        public string waterLayer;
    }
}