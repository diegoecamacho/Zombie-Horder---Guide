using System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CameraControls : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float RotationPower = 10;
        [SerializeField] private float HorizontalDamping = 1;
        [SerializeField] private GameObject FollowTarget;
        [SerializeField] private float PlayerYDampening;
        
        //Transform References
        private Transform FollowTargetTransform;
        
        //Components
        private Camera ViewCamera;
        private Animator PlayerAnimator;
        private CrossHairFollowMouse CrossHairFollow;
        
        private Vector2 AimValue;
        
        private Vector2 PreviousFrameMouseInput;


        // Start is called before the first frame update
        private void Awake()
        {
            ViewCamera = Camera.main;
            PlayerAnimator = GetComponent<Animator>();
            CrossHairFollow = GetComponent<PlayerController>().CrossHairComponent;

            //Cached Values
            FollowTargetTransform = FollowTarget.transform;
        }

        private void OnLook(InputValue value)
        {
            AimValue = value.Get<Vector2>();
            
            Quaternion addedRotation = Quaternion.AngleAxis(
                Mathf.Lerp(PreviousFrameMouseInput.x, AimValue.x, 1f / HorizontalDamping) * RotationPower,
                Vector3.up);
            

            //Rotate Around a certain on Aim Value
            FollowTargetTransform.rotation *= addedRotation;
                    
                 
            PreviousFrameMouseInput = AimValue;
                
            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, FollowTargetTransform.transform.rotation.eulerAngles.y - PlayerYDampening, 0);
            
            //reset the follow rotation to center the camera
            FollowTargetTransform.localEulerAngles = Vector3.zero;
        }
        
    }
}