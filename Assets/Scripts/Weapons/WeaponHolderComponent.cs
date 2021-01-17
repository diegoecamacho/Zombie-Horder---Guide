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

        //Components
        private PlayerController PlayerController;
        private Animator PlayerAnimator;
        private WeaponComponent WeaponComponent;
        private CrossHairFollowMouse CrossHairFollow;

        //Animator Hashcode
        private readonly int WeaponFiringHash =  Animator.StringToHash("isFiring");
        private readonly int WeaponReloadingHash =  Animator.StringToHash("isReloading");

        private void Awake()
        {
            PlayerAnimator = GetComponent<Animator>();
            PlayerController = GetComponent<PlayerController>();
            
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
            WeaponEvents.Invoke_OnWeaponEquipped(WeaponComponent);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKLocation.position);
        }
    
        //Input Actions
        public void OnFire(InputValue value)
        {
            if (PlayerController.IsReloading) return;
            
            if (value.isPressed)
            {
                PlayerController.IsFiring = true;
                PlayerAnimator.SetBool(WeaponFiringHash, PlayerController.IsFiring);
                WeaponComponent.BeginFiringWeapon();
            }
            else
            {
                PlayerController.IsFiring = false;
                PlayerAnimator.SetBool(WeaponFiringHash, PlayerController.IsFiring);
                WeaponComponent.EndFiringWeapon();
            }
        }
        
        public void OnReloading(InputValue value)
        {
            if (WeaponComponent.Reloading) return;
            StartReloading();
        }

        public void StartReloading()
        {
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
        }
    }
}