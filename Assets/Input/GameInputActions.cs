// GENERATED AUTOMATICALLY FROM 'Assets/Input/GameInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputActions"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""7b6319de-4d34-4bbb-8692-e627c4067ab6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""cd19fd6c-92b6-4166-8603-293da0276f6e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""662af67a-73d4-43bd-861c-7c18fb42053e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""3d1b8c44-49d1-4edc-a1e7-35c4635e7701"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""70c3d54e-4b00-408f-9327-badd1a536887"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""b7b38d6a-2c56-4623-9e3b-889fd253b4a9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reloading"",
                    ""type"": ""Button"",
                    ""id"": ""ec2e84c5-a73d-4fb1-bea6-2230a8f1e654"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""be01304e-d07f-430f-8269-7d76861a1f50"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""af17e2ab-9699-4d12-bb8d-5a09801eca68"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""34ebab16-78c5-4621-90ac-2fa0ea480f56"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""43618fb9-7f01-4f66-b9ee-8f8c802900b4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a1004bd0-e922-4ca7-924a-859b742f9e90"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce58180c-e933-4dd7-82b4-5160e3756045"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e027de8e-c02a-40d3-a98d-d123d84bca79"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3fd98cc2-7c2c-4a05-81e8-137ab9394a08"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70a87866-a90d-40f4-b4de-e1c4a86e916b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""057764ba-8389-4b91-b6bb-3d6b8696cc0b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfb6b12c-f599-4175-bde1-08df9cc08af4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Reloading"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4df0721a-e725-4457-a79e-4141a787ef98"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Movement = m_CharacterControls.FindAction("Movement", throwIfNotFound: true);
        m_CharacterControls_Run = m_CharacterControls.FindAction("Run", throwIfNotFound: true);
        m_CharacterControls_Aim = m_CharacterControls.FindAction("Aim", throwIfNotFound: true);
        m_CharacterControls_Fire = m_CharacterControls.FindAction("Fire", throwIfNotFound: true);
        m_CharacterControls_Look = m_CharacterControls.FindAction("Look", throwIfNotFound: true);
        m_CharacterControls_Reloading = m_CharacterControls.FindAction("Reloading", throwIfNotFound: true);
        m_CharacterControls_Jump = m_CharacterControls.FindAction("Jump", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private ICharacterControlsActions m_CharacterControlsActionsCallbackInterface;
    private readonly InputAction m_CharacterControls_Movement;
    private readonly InputAction m_CharacterControls_Run;
    private readonly InputAction m_CharacterControls_Aim;
    private readonly InputAction m_CharacterControls_Fire;
    private readonly InputAction m_CharacterControls_Look;
    private readonly InputAction m_CharacterControls_Reloading;
    private readonly InputAction m_CharacterControls_Jump;
    public struct CharacterControlsActions
    {
        private @GameInputActions m_Wrapper;
        public CharacterControlsActions(@GameInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CharacterControls_Movement;
        public InputAction @Run => m_Wrapper.m_CharacterControls_Run;
        public InputAction @Aim => m_Wrapper.m_CharacterControls_Aim;
        public InputAction @Fire => m_Wrapper.m_CharacterControls_Fire;
        public InputAction @Look => m_Wrapper.m_CharacterControls_Look;
        public InputAction @Reloading => m_Wrapper.m_CharacterControls_Reloading;
        public InputAction @Jump => m_Wrapper.m_CharacterControls_Jump;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Run.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Aim.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnAim;
                @Fire.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFire;
                @Look.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @Reloading.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnReloading;
                @Reloading.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnReloading;
                @Reloading.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnReloading;
                @Jump.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_CharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Reloading.started += instance.OnReloading;
                @Reloading.performed += instance.OnReloading;
                @Reloading.canceled += instance.OnReloading;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface ICharacterControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnReloading(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
