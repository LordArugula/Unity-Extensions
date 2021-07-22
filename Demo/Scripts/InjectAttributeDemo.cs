using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public interface IService { }

    public class FooService : IService { }

    public class BarService : IService { }

    public class InjectAttributeDemo : MonoBehaviour
    {
        [Inject]
        [SerializeReference]
        public IService service;
    }
}
