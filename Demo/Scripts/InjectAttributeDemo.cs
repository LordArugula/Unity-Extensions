using System.Collections.Generic;
using UnityEngine;

namespace Arugula.Extensions.Demos
{
    public interface IService { }

    [System.Serializable]
    public class FooService : IService
    {
        [Inject]
        [SerializeReference]
        private IService service;
    }

    [System.Serializable]
    public class BarService : IService
    {
        public string value;
    }

    public interface IProvider { }

    [System.Serializable]
    public class BarProvider : IProvider
    {
        public int value;
    }

    public abstract class Character
    {
        public string name;
    }

    [System.Serializable]
    public class Player : Character
    {
        public bool isGrounded;
    }

    [System.Serializable]
    public class Enemy : Character
    {
        public GameObject stuff;
    }

    public class InjectAttributeDemo : MonoBehaviour
    {
        [Inject]
        [SerializeReference]
        private List<IService> services;

        [Inject]
        [SerializeReference]
        private Character character;
    }
}
