using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Input
{
    public class DesktopInput : IInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        
        private readonly KeyCode FireButton  = KeyCode.Mouse0;
        
        public bool IsEnabled { get; set; } = true;

        public Vector3 Direction
        {
            get
            {
                if (IsEnabled == false)
                    return Vector3.zero;

                Vector3 direction = new Vector3(UnityEngine.Input.GetAxisRaw(HorizontalAxisName), 0,
                    UnityEngine.Input.GetAxisRaw(VerticalAxisName));

                return direction;
            }
        }

        public bool Fire
        {
            get
            {
                if (IsEnabled == false)
                    return false;

                return UnityEngine.Input.GetKeyDown(FireButton);
            }
        }
        
    }
}