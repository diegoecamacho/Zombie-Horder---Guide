using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Character
{
    /// <summary>
    ///     Movement Controller
    ///     Handles Player Movement and Aim
    /// </summary>
    public class MovementComponent : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float MovementSpeed;
        [SerializeField] private float RunSpeed;
        [SerializeField] private float JumpForce;
        
        //Movement
        public Vector3 MoveDirection { get; private set; }
        private Vector3 MovementVector;

        //Components
        private Camera CameraRef;
        private NavMeshAgent NavAgent;
        private Animator PlayerAnimator;
        private PlayerController PlayerController;
        private Rigidbody Rigidbody;
        
        //Animator Hashes (Performance Optimization)
        private readonly int MovementXHash = Animator.StringToHash("movementX");
        private readonly int MovementYHash = Animator.StringToHash("movementY");
        private readonly int IsJumpingHash = Animator.StringToHash("isJumping");
        private readonly int IsRunningHash = Animator.StringToHash("isRunning");
        
        
        // Transform Caches
        private Transform PlayerTransform;
        
        private void Awake()
        {
            PlayerController = GetComponent<PlayerController>();
            PlayerAnimator = GetComponent<Animator>();
            NavAgent = GetComponent<NavMeshAgent>();
            Rigidbody = GetComponent<Rigidbody>();

            CameraRef = Camera.main;
            PlayerTransform = transform;
        }

        private void Update()
        {
            //Make sure magnitude is greater than one, Helps make sure animations are working properly
            if (!(MovementVector.magnitude > 0)) MovementVector = Vector3.zero;

            //Make sure player is not jumping while trying to move
            if (PlayerController.IsJumping) return;

            MoveDirection = PlayerTransform.forward * MovementVector.z + PlayerTransform.right * MovementVector.x;

            float currentSpeed = PlayerController.IsRunning ? RunSpeed : MovementSpeed;
            Vector3 movementDirection = MoveDirection * (currentSpeed * Time.deltaTime);
            
            #region TransformMovement

            //PlayerTransform.position += movementDirection;

            #endregion
            
           #region NavMeshMovement

           NavAgent.Move(movementDirection);

           #endregion
        }

        #region Player Movement
        public void OnMovement(InputValue value)
        {
            Vector2 playerMovement = value.Get<Vector2>();
            HandleMovement(playerMovement);
        }

        private void HandleMovement(Vector2 movement)
        {
            MovementVector = new Vector3(movement.x, 0, movement.y);

            //Set Animator Movement Variables
            PlayerAnimator.SetFloat(MovementYHash, movement.y);
            PlayerAnimator.SetFloat(MovementXHash, movement.x);
        }
        
        #endregion

        #region Player Run

        public void OnRun(InputValue value)
        {
            PlayerController.IsRunning = value.isPressed;
            PlayerAnimator.SetBool(IsRunningHash, PlayerController.IsRunning);
        }

        #endregion
        
        #region Player Jump

        public void OnJump(InputValue value)
        {
            if (!value.isPressed) return;
            if (PlayerController.IsJumping) return;
                
            PlayerController.IsJumping = true;
            PlayerAnimator.SetBool(IsJumpingHash, PlayerController.IsJumping);
            Rigidbody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);

            #region NavMeshMovement

            //Nav Mesh has to be disabled if you want to jump with NavMeshes
            NavAgent.enabled = false;

            #endregion
        }


        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ground")) return;

            PlayerController.IsJumping = false;
            PlayerAnimator.SetBool(IsJumpingHash, PlayerController.IsJumping);

            #region NavMeshMovement

            NavAgent.enabled = true;

            #endregion
        }

        #endregion
        
        #region NavMeshMovement

        public void SetDestination(Vector3 position)
        {
            NavAgent.SetDestination(position);
        }

        #endregion
    }
}