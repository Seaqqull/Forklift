using UnityEngine;


namespace Forklift.General
{
    public class Fork : Base.BaseBehaviour
    {
        [SerializeField] private float _forkPower;

        public Item ItemToPickup { get; private set; }
        public float Power { get => _forkPower; }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Item>(out var item))
                return;

            ItemToPickup = item;
        }
    }
}