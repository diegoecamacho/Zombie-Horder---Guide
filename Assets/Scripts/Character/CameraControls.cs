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

        private Vector2 PreviousFrameMouseInput;
       
        //Transform References
        private Transform FollowTargetTransform;
        
        //Components
        private Camera ViewCamera;
        private Animator PlayerAnimator;
        private CrossHairFollowMouse CrossHairFollow;
        
        //Animation Hashes
        private readonly int VerticalAim =  Animator.StringToHash("aimVertical");
        private readonly int HorizontalAim = Animator.StringToHash("aimHorizontal");
        
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
            Vector2 aimValue = value.Get<Vector2>();

            //Rotate Around a certain on Aim Value
            FollowTargetTransform.rotation *=
                Quaternion.AngleAxis(
                    Mathf.Lerp(PreviousFrameMouseInput.x, aimValue.x, 1f / HorizontalDamping) * RotationPower,
                    Vector3.up);

            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, FollowTargetTransform.transform.rotation.eulerAngles.y, 0);

            //reset the follow rotation to center the camera
            FollowTargetTransform.localEulerAngles = Vector3.zero;

            PreviousFrameMouseInput = aimValue;

            //Animation Setup
            Vector3 independentMousePosition = ViewCamera.ScreenToViewportPoint(CrossHairFollow.CurrentAimPosition);

            PlayerAnimator.SetFloat(VerticalAim,
                CrossHairFollow.Inverted ? independentMousePosition.y : 1f - independentMousePosition.y);
            PlayerAnimator.SetFloat(HorizontalAim, independentMousePosition.x);
        }
    }
}