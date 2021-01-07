using Cinemachine;
using Helpers;
using Parents;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CameraControls : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float RotationPower = 10;

        [SerializeField] private float HorizontalDamping = 1;

        [SerializeField] private Transform FollowTarget;

        private Animator PlayerAnimator;

        private Vector2 PreviousFrameMouseInput;

        [Header("Components")]
        //Components
        private Camera ViewCamera;

        private CrossHairFollowMouse CrossHairFollow;


        //Animation Hashes
        private int VerticalAim;
        private int HorizontalAim;

        // Start is called before the first frame update
        private void Awake()
        {
            ViewCamera = Camera.main;
            PlayerAnimator = GetComponent<Animator>();
            CrossHairFollow = GetComponent<PlayerController>().CrossHairComponent;

            //Animation Hashes Initialization
            VerticalAim = Animator.StringToHash("aimVertical");
            HorizontalAim = Animator.StringToHash("aimHorizontal");
        }

        private void OnLook(InputValue value)
        {
            Vector2 aimValue = value.Get<Vector2>();

            Transform followTransform = FollowTarget.transform;

            followTransform.rotation *=
                Quaternion.AngleAxis(
                    Mathf.Lerp(PreviousFrameMouseInput.x, aimValue.x, 1f / HorizontalDamping) * RotationPower,
                    Vector3.up);

            Vector3 angles = followTransform.localEulerAngles;
            angles.z = 0;

            float angle = followTransform.localEulerAngles.x;

            //Clamp the Up/Down rotation
            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }

            FollowTarget.transform.localEulerAngles = angles;

            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

            //reset the y rotation of the look transform
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

            PreviousFrameMouseInput = aimValue;

            //Animation Setup
            Vector3 independentMousePosition = ViewCamera.ScreenToViewportPoint(CrossHairFollow.CurrentLookPosition);

            PlayerAnimator.SetFloat(VerticalAim,
                CrossHairFollow.Inverted ? independentMousePosition.y : 1f - independentMousePosition.y);
            PlayerAnimator.SetFloat(HorizontalAim, independentMousePosition.x);
        }
    }
}