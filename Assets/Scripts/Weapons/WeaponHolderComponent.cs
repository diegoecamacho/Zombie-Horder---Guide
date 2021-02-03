using Character;
using Events;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    /// <summary>
    /// Weapon Holder
    /// Handles Weapon Positioning and Firing
    /// </summary>
    public class WeaponHolderComponent : MonoBehaviour
    {
        [Header("Spawn Weapon")] [SerializeField]
        private GameObject WeaponToSpawn;

        [Header("Bone Positions")] [SerializeField]
        private Transform WeaponSpawnLocation;
        
        private Transform GripIKLocation;
        private bool WasFiring = false;
        private bool PlayerShootingInput;

        //Components
        public PlayerController PlayerController;
        private Animator PlayerAnimator;
        private WeaponComponent WeaponComponent;
        private CrossHairFollowMouse CrossHairFollow;
        
        //Ref
        private Camera ViewCamera;

        //Animator Hashcode
        private readonly int WeaponFiringHash =  Animator.StringToHash("isFiring");
        private readonly int WeaponReloadingHash =  Animator.StringToHash("isReloading");
        private readonly int WeaponTypeHash = Animator.StringToHash("WeaponType");
        private static readonly int VerticalAimHash = Animator.StringToHash("aimVertical");
        private static readonly int HorizontalAimHash = Animator.StringToHash("aimHorizontal");

        private void Awake()
        {
            PlayerAnimator = GetComponent<Animator>();
            PlayerController = GetComponent<PlayerController>();
            
            ViewCamera= Camera.main;

            if (PlayerController) CrossHairFollow = PlayerController.CrossHairComponent;
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            GameObject spawnedWeapon =
                Instantiate(WeaponToSpawn, WeaponSpawnLocation.position, WeaponSpawnLocation.rotation);

            if (!spawnedWeapon) return;
   
            //Set Weapon in Weapon Socket
            spawnedWeapon.transform.parent = WeaponSpawnLocation;

            //Get Weapon Component
            WeaponComponent = spawnedWeapon.GetComponent<WeaponComponent>();
            
            if (!WeaponComponent) return;
            //Set IK Location for weapon holding.
            GripIKLocation = WeaponComponent.GripHandPlacementLocation;
            
            //Initialize Weapon
            WeaponComponent.Initialize(this, CrossHairFollow);
            
            PlayerAnimator.SetInteger(WeaponTypeHash, (int)WeaponComponent.WeaponInfo.WeaponType);
            WeaponEvents.Invoke_OnWeaponEquipped(WeaponComponent);
        }

        public void OnLook(InputValue delta)
        {
            Vector3 independentMousePosition = ViewCamera.ScreenToViewportPoint(CrossHairFollow.CurrentAimPosition);
            PlayerAnimator.SetFloat(VerticalAimHash,
                CrossHairFollow.Inverted ? independentMousePosition.y : 1f - independentMousePosition.y);
            PlayerAnimator.SetFloat(HorizontalAimHash, independentMousePosition.x);
        }

        //Input Actions
        public void OnFire(InputValue button)
        {
            PlayerShootingInput = button.isPressed;
            if (PlayerController.IsReloading) return;
            
            if (button.isPressed)
                StartFiring();
            else
                StopFiring();
        }
        
        public void OnReloading(InputValue value)
        {
            //If already reloading or no bullets available simply return
            if (WeaponComponent.Reloading || WeaponComponent.WeaponInfo.TotalBulletsAvailable == 0) return;
            StartReloading();
        }

        private void StartFiring()
        {
            PlayerController.IsFiring = true;
            PlayerAnimator.SetBool(WeaponFiringHash, PlayerController.IsFiring);
            WeaponComponent.BeginFiringWeapon();
        }

        private void StopFiring()
        {
            PlayerController.IsFiring = false;
            PlayerAnimator.SetBool(WeaponFiringHash, PlayerController.IsFiring);
            WeaponComponent.EndFiringWeapon();
        }

        public void StartReloading()
        {
            if (PlayerController.IsFiring)
            {
                WasFiring = true;
                StopFiring();
            }

            PlayerController.IsReloading = true;
            PlayerAnimator.SetBool(WeaponReloadingHash, PlayerController.IsReloading);
            WeaponComponent.StartReloading();
           
            InvokeRepeating(nameof(StopReloading), 0, 0.1f);
        }

        public void StopReloading()
        {
            if (PlayerAnimator.GetBool(WeaponReloadingHash)) return;
            
            PlayerController.IsReloading = false;
            WeaponComponent.StopReloading();
            CancelInvoke(nameof(StopReloading));

            if (WasFiring && PlayerShootingInput)
            {
                StartFiring();
                WasFiring = false;
            }
        }
        
        private void OnAnimatorIK(int layerIndex)
        {
            PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKLocation.position);
        }
    }
}