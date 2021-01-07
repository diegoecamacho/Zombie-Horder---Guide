using System;
using System.Drawing;
using Parents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class CrossHairFollowMouse : InputMonoBehaviour
    {
        public Vector2 CurrentLookPosition { get; private set; }

        [Header("Settings")] public Vector2 MouseSensitivity;
        public bool Inverted;
        public bool DisableCursor;


        [Header("Angles")] [SerializeField] private float MaxAngleVertical;
        [SerializeField] private float MinAngleVertical;

        [SerializeField] private float MaxAngleHorizontal;
        [SerializeField] private float MinAngleHorizontal;

        private Vector2 CrossHairStartingPosition;

        private Vector2 CurrentLookDeltas;

        private Vector2 MouseDelta;

        private Camera Camera;


        private new void Awake()
        {
            base.Awake();

            Camera = Camera.main;

            if (!DisableCursor) return;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            CrossHairStartingPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }


        private void OnLook(InputAction.CallbackContext value)
        {
            MouseDelta = value.ReadValue<Vector2>();
            CurrentLookDeltas.x += MouseDelta.x;
            if (CurrentLookDeltas.x > MaxAngleHorizontal || CurrentLookDeltas.x < MinAngleHorizontal)
            {
                CurrentLookDeltas.x -= MouseDelta.x;
            }

            CurrentLookDeltas.y += MouseDelta.y;
            if (CurrentLookDeltas.y > MaxAngleVertical || CurrentLookDeltas.y < MinAngleVertical)
            {
                CurrentLookDeltas.y -= MouseDelta.y;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            float invertedValue = Inverted
                ? CrossHairStartingPosition.y + CurrentLookDeltas.y * MouseSensitivity.y
                : CrossHairStartingPosition.y - CurrentLookDeltas.y * MouseSensitivity.y;
            CurrentLookPosition = new Vector2(CrossHairStartingPosition.x + CurrentLookDeltas.x * MouseSensitivity.x,
                invertedValue);

            transform.position = CurrentLookPosition;
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