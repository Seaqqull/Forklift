using UnityEngine;


namespace Forklift.General
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private float _weight;

        public float Weight
        {
            get => _weight;
        }
    }
}