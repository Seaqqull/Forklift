using System;
using UnityEngine;


namespace Forklift.General
{
    public class Item : Base.BaseBehaviour
    {
        [SerializeField] private float _weight;
        [Header("Ground")] 
        [SerializeField] private Transform _groundPivot;
        [SerializeField] private float _groundDistance;
        [SerializeField] private LayerMask _ground;

        private Action _onDetach;
        
        public event Action OnDetach
        {
            add { _onDetach += value; }
            remove { _onDetach -= value; }
        }
        public bool Attached { get; private set; }
        public float Weight
        {
            get => _weight;
        }


        private void LateUpdate()
        {
            if (!Attached)
                return;
            
            bool grounded = (Physics.Raycast(_groundPivot.position, Vector3.down, _groundDistance, _ground)); // raycast down to look for ground is not detecting ground? only works if allowing jump when grounded = false; // return "Ground" layer as layer
            if (grounded)
                Detach();
        }

        
        public virtual void Detach()
        {
            _onDetach?.Invoke();
            Transform.parent = null;
            Attached = false;
        }

        public virtual void Attach(Transform pivot)
        {
            Transform.parent = pivot;
            Attached = true;
        }
    }
}