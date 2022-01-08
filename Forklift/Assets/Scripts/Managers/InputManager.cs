using UnityEngine;


namespace Forklift.Managers
{
    public class InputManager : Base.SingleBehaviour<InputManager>
    {
        public float Horizontal
        {
            get; private set;
        }
        public float Vertical
        {
            get; private set;
        }
        public bool Space
        {
            get; private set;
        }
        public bool Shift
        {
            get; private set;
        }
        
        
        private void Update()
        {
            // General
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
            
            // Special
            Shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            Space = Input.GetKey(KeyCode.Space);
        }
    }
}