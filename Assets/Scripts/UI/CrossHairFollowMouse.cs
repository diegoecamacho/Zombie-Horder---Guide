using System;
using System.Drawing;
using Parents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class CrossHairFollowMouse : InputMonoBehaviour
    {
        public Vector2 CurrentAimPosition { get; private set; }

        [Header("Settings")] public Vector2 MouseSensitivity;
        public bool Inverted;
        public bool DisableCursor;
        
        [SerializeField, Range(0f , 0.5f)] private float CrosshairHorizontalPercentage = 0.25f;
        private float HorizontalOffset = 0f;
        private float MaxHorizontalAngle = 0f;
        private float MinHorizontalAngle = 0f;
         
        [SerializeField, Range(0f , 0.5f)] private float CrosshairVerticalPercentage = 0.25f;
        private float VerticalOffset = 0f;
        private float MaxVerticalAngle = 0f;
        private float MinVerticalAngle = 0f;

        private Vector2 CrossHairStartingPosition;
        private Vector2 CurrentLookDeltas;

        private new void Awake()
        {
            base.Awake();

            if (!DisableCursor) return;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            CrossHairStartingPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);

            //Crosshair Constrains Setup
            HorizontalOffset = Screen.width * CrosshairHorizontalPercentage;
            MinHorizontalAngle = -(Screen.width / 2f) + HorizontalOffset;
            MaxHorizontalAngle = (Screen.width / 2f) - HorizontalOffset;

            Debug.Log($"{MinHorizontalAngle} :: {MaxHorizontalAngle}");
            
            VerticalOffset = Screen.height * CrosshairVerticalPercentage;
            MinVerticalAngle = -(Screen.height / 2f) + VerticalOffset;
            MaxVerticalAngle = (Screen.height / 2f) - VerticalOffset;
        }
        
        private void OnLook(InputAction.CallbackContext delta)
        {
            //Get Mouse Delta
           Vector2 mouseDelta = delta.ReadValue<Vector2>();
            
            //Clamp X Coordinates
            CurrentLookDeltas.x += mouseDelta.x * MouseSensitivity.x;
            if (CurrentLookDeltas.x >= MaxHorizontalAngle || CurrentLookDeltas.x <= MinHorizontalAngle)
            {
                CurrentLookDeltas.x -= mouseDelta.x * MouseSensitivity.x;
            }
            
            //Clamp Y Coordinates
            CurrentLookDeltas.y += mouseDelta.y * MouseSensitivity.y;
            if (CurrentLookDeltas.y >= MaxVerticalAngle || CurrentLookDeltas.y <= MinVerticalAngle)
            {
                CurrentLookDeltas.y -= mouseDelta.y * MouseSensitivity.y;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            float crosshairXPosition = CrossHairStartingPosition.x + CurrentLookDeltas.x;
            float crosshairYPosition = Inverted
                ? CrossHairStartingPosition.y + CurrentLookDeltas.y
                : CrossHairStartingPosition.y - CurrentLookDeltas.y;

            CurrentAimPosition = new Vector2(crosshairXPosition,
                crosshairYPosition);
            
            transform.position = CurrentAimPosition;
        }

        private new void OnEnable()
        {
            base.OnEnable();
            GameInput.CharacterControls.Look.performed += OnLook;
        }

        private new void OnDisable()
        {
            base.OnDisable();
            GameInput.CharacterControls.Look.performed -= OnLook;
        }
    }
}