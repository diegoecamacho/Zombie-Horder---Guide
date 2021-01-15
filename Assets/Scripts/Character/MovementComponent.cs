using System;
using UnityEngine;
using UnityEngine.AI;
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
        [Header("Settings")] [SerializeField] private float MovementSpeed;
        [Header("Settings")] [SerializeField] private float JumpForce;

        public Vector3 MoveDirection { get; private set; }


        private Animator PlayerAnimator;
        private NavMeshAgent NavAgent;
        private Rigidbody Rigidbody;

        private Vector3 MovementVector;

        private Camera CameraRef;

        private Transform PlayerTransform;

        private bool IsJumping = false;

        //Animator Hashes (Performance Optimization)
        private int MovementXHash;
        private int MovementYHash;
        private int IsRunningHash;


        private void Awake()
        {
            PlayerAnimator = GetComponent<Animator>();
            NavAgent = GetComponent<NavMeshAgent>();
            Rigidbody = GetComponent<Rigidbody>();

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
            if (!(MovementVector.magnitude > 0)) MovementVector = Vector3.zero;
            if (IsJumping) return;

            MoveDirection = PlayerTransform.forward * MovementVector.z + PlayerTransform.right * MovementVector.x;
            Vector3 movementDirection = MoveDirection * (MovementSpeed * Time.deltaTime);

            NavAgent.Move(movementDirection);

            //PlayerTransform.position += PlayerTransform.forward * (MovementVector.z * MovementSpeed * Time.deltaTime) 
            //+ PlayerTransform.right * (MovementVector.x * MovementSpeed * Time.deltaTime);
        }

        public void SetDestination(Vector3 position)
        {
            NavAgent.SetDestination(position);
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

        public void OnJump(InputValue value)
        {
            if (!value.isPressed) return;

            Debug.Log("Jump");
            IsJumping = true;
            NavAgent.enabled = false;
            Rigidbody.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ground")) return;
            
            IsJumping = false;
            NavAgent.enabled = true;
        }
    }
}