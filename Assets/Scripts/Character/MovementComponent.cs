using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Character
{
    
    /// <summary>
    /// Movement Controller
    /// Handles Player Movement and Aim
    /// </summary>
    public class MovementComponent : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float MovementSpeed;
        
        private Animator PlayerAnimator;

        private Vector3 MovementVector;
        
        private Camera CameraRef;

        private Transform PlayerTransform;
        
        //Animator Hashes (Performance Optimization)
        private int MovementXHash;
        private int MovementYHash;
        private int IsRunningHash;
   
        
        private new void Awake()
        {
            PlayerAnimator = GetComponent<Animator>();
            
            CameraRef = Camera.main;
            
            //Hash Setup.
            MovementXHash = Animator.StringToHash("movementX");
            MovementYHash = Animator.StringToHash("movementY");
            IsRunningHash = Animator.StringToHash("isRunning");
  
        }
        private void Start()
        {
            PlayerTransform = transform;
        }
        
        private void HandleMovement(Vector2 movement)
        {
            MovementVector = new Vector3(movement.x, 0, movement.y);

            //Set Animator Movement Variables
            PlayerAnimator.SetFloat(MovementYHash, movement.y);
            PlayerAnimator.SetFloat(MovementXHash, movement.x);
        }
        private void Update()
        {
         

            if (!(MovementVector.magnitude > 0)) return;
            
            PlayerTransform.position += PlayerTransform.forward * (MovementVector.z * MovementSpeed * Time.deltaTime) 
            + PlayerTransform.right * (MovementVector.x * MovementSpeed * Time.deltaTime);
        }
        
        //Player Input Functions
        public void OnMovement(InputValue value)
        {
            Vector2 playerMovement = value.Get<Vector2>();
            HandleMovement(playerMovement);
        }

        public void OnRun(InputValue value)
        {
            Debug.Log(value.isPressed);
        }
    
    }
}