using UnityEngine;

namespace Parents
{
    public abstract class InputMonoBehaviour : MonoBehaviour
    {
        protected GameInputActions GameInput;

        protected void Awake()
        {
            GameInput = new GameInputActions();
        }

        protected void OnEnable()
        {
            GameInput.Enable();
        }

        protected void OnDisable()
        {
            GameInput.Disable();
        }
    }
}
