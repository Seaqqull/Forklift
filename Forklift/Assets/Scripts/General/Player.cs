using Forklift.Managers;
using UnityEngine.AI;
using UnityEngine;


namespace Forklift.General
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Player : MonoBehaviour
    {
        [Header("Motion")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] [Range(0.0f, 360.0f)] private float _rotationSpeedInPlase;
        [SerializeField] [Range(0.0f, 360.0f)] private float _rotationSpeedInMovement;

        
        private Transform _transform;
        private NavMeshAgent _agent;


        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var horizontalInput = Mathf.Approximately(Managers.InputManager.Instance.Horizontal, 0.0f);
            var verticalInput = Mathf.Approximately(Managers.InputManager.Instance.Vertical, 0.0f);
            
            if (!verticalInput)
            {
                _agent.destination = _transform.position + _transform.rotation *
                    (Vector3.back * InputManager.Instance.Vertical * _movementSpeed) * Time.deltaTime;
            }
            if(!horizontalInput)
            {
                _transform.Rotate(
                    Vector3.up, 
                    (InputManager.Instance.Horizontal * (verticalInput? _rotationSpeedInPlase : _rotationSpeedInMovement) * Time.deltaTime)
                );
            }
        }
    }
}