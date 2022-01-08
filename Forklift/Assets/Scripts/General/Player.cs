using Forklift.Managers;
using UnityEngine.AI;
using UnityEngine;


namespace Forklift.General
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Player : Base.BaseBehaviour
    {
        [Header("Motion")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] [Range(0.0f, 360.0f)] private float _rotationSpeedInPlase;
        [SerializeField] [Range(0.0f, 360.0f)] private float _rotationSpeedInMovement;
        [Header("Fork")] 
        [SerializeField] private Fork _fork;
        [SerializeField] private float _forkLiftTime;
        [SerializeField] [Range(0.3f, 10.0f)] private float _liftHeight;
        
        private NavMeshAgent _agent;
        private Vector3 _liftPositionBase;
        private float _liftProgress;


        protected override void Awake()
        {
            base.Awake();
            
            _agent = GetComponent<NavMeshAgent>();
            if (_fork == null)
                _fork = GetComponentInChildren<Fork>();
            if (_fork == null)
                return;
            _liftPositionBase = _fork.Transform.localPosition;
        }

        private void Update()
        {
            var horizontalInput = Mathf.Approximately(Managers.InputManager.Instance.Horizontal, 0.0f);
            var verticalInput = Mathf.Approximately(Managers.InputManager.Instance.Vertical, 0.0f);
            
            // Movement
            if (!verticalInput)
            {
                _agent.destination = Transform.position + Transform.rotation *
                    (Vector3.back * InputManager.Instance.Vertical * _movementSpeed) * Time.deltaTime;
            }
            // Rotation
            if(!horizontalInput)
            {
                var reverseDirection = verticalInput && (Managers.InputManager.Instance.Vertical < 0.0f);
                var rotationAmount = InputManager.Instance.Horizontal *
                                     (verticalInput ? _rotationSpeedInPlase : _rotationSpeedInMovement) *
                                     (reverseDirection ? -1.0f : 1.0f) *
                                     Time.deltaTime;
                Transform.Rotate(Vector3.up, rotationAmount);
            }

            
            // Pickup
            if (InputManager.Instance.Space)
            {
                // Up
                if (!InputManager.Instance.Shift && _liftProgress < 1.0f)
                {
                    if ((_fork.ItemToPickup != null) && (_fork.ItemToPickup.Weight > _fork.Power))
                        return;
                    _liftProgress += (Time.deltaTime * _forkLiftTime);
                    Mathf.Clamp(_liftProgress, 0.0f, 1.0f);

                    _fork.Transform.localPosition = _liftPositionBase + (Vector3.up * _liftProgress * _liftHeight);
                }
                if (InputManager.Instance.Shift && _liftProgress > 0.0f)
                {
                    _liftProgress -= (Time.deltaTime * _forkLiftTime);
                    Mathf.Clamp(_liftProgress, 0.0f, 1.0f);
                    
                    _fork.Transform.localPosition = _liftPositionBase + (Vector3.up * _liftProgress * _liftHeight);
                }
            }

            if (InputManager.Instance.Space && _fork.ItemToPickup != null)
            {
                Debug.Log(_fork.ItemToPickup);
            }
        }
    }
}