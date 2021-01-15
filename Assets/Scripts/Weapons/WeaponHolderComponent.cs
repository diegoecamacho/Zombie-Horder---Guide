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

        private Animator PlayerAnimator;

        private Transform GripIKLocation;

        private WeaponComponent WeaponComponent;
        
        private CrossHairFollowMouse CrossHairFollow;

        //Animator Hashcode
        private int WeaponFiringHash;
        private int WeaponReloadingHash;

        private void Awake()
        {
            PlayerAnimator = GetComponent<Animator>();
            CrossHairFollow = GetComponent<PlayerController>().CrossHairComponent;
            
            //Animation Hash
            WeaponFiringHash = Animator.StringToHash("firing");
            WeaponReloadingHash = Animator.StringToHash("reloading");
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            var spawnedWeapon =
                Instantiate(WeaponToSpawn, WeaponSpawnLocation.position, WeaponSpawnLocation.rotation);
            spawnedWeapon.transform.parent = WeaponSpawnLocation;

            WeaponComponent = spawnedWeapon.GetComponent<WeaponComponent>();
            
            GripIKLocation = WeaponComponent.GripHandPlacementLocation;

            if (!WeaponComponent) return;

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
            if (value.isPressed)
            {
                PlayerAnimator.SetBool(WeaponFiringHash, true);
                WeaponComponent.BeginFiringWeapon();
            }
            else
            {
                PlayerAnimator.SetBool(WeaponFiringHash, false);
                WeaponComponent.EndFiringWeapon();
            }
        }
        
        public void OnReloading(InputValue value)
        {
            if (!WeaponComponent.Reloading)
            {
                StartReloading();
            }
        }

        public void StartReloading()
        {
            PlayerAnimator.SetBool(WeaponReloadingHash, true);
           WeaponComponent.StartReloading();
           
           InvokeRepeating(nameof(StopReloading), 0, 0.1f);
        }

        public void StopReloading()
        {
            if (PlayerAnimator.GetBool(WeaponReloadingHash)) return;
            
            WeaponComponent.StopReloading();
            CancelInvoke(nameof(StopReloading));
        }
    }
}