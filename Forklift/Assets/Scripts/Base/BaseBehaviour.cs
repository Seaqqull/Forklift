using UnityEngine;


namespace Forklift.Base
{
    public class BaseBehaviour : MonoBehaviour
    {
        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }


        protected virtual void Awake()
        {
            GameObject = gameObject;
            Transform = transform;
        }
    }
}