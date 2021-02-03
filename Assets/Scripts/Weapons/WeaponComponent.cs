using Events;
using Interfaces;
using Parents;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class WeaponComponent : MonoBehaviour
    {
        public Transform GripHandPlacementLocation => GripLocation;
        public Transform BarrelLocation => BarrelLocationPoint;
        public bool WeaponFiring { get; private set; } = false;
        public bool Reloading { get; private set; }

        public WeaponEvents.WeaponStats WeaponInfo => WeaponInformation;

        [SerializeField] private Transform GripLocation;

        [SerializeField] private Transform BarrelLocationPoint;

        [SerializeField] private GameObject FiringAnimation;

        [SerializeField] private WeaponEvents.WeaponStats WeaponInformation;

        private CrossHairFollowMouse CrossHairFollow;
        private WeaponHolderComponent WeaponHolder;

        /// <summary>
        /// Direction Mouse is aiming at in comparison to barrel
        /// </summary>
        private Vector3 AimWorldDirection;

        /// <summary>
        /// Game Camera
        /// </summary>
        private Camera ViewCamera;

        private ParticleSystem FiringEffect;
        
        //Debug
        private RaycastHit HitLocation;

        private void Awake()
        {
            ViewCamera = Camera.main;
        }

        public void Initialize(WeaponHolderComponent weaponHolderComponent, CrossHairFollowMouse crossHairFollowMouse)
        {
            WeaponHolder = weaponHolderComponent;
            CrossHairFollow = crossHairFollowMouse;
        }

        private void FireWeapon()
        {
            if (WeaponInformation.BulletsInClip > 0 && !Reloading && !WeaponHolder.PlayerController.IsRunning)
            {
                if (!FiringEffect)
                {
                    FiringEffect = Instantiate(FiringAnimation, BarrelLocation).GetComponent<ParticleSystem>();
                }
                
                //Shoot Ray at Crosshair position
                Ray screenRay = ViewCamera.ScreenPointToRay(new Vector3(CrossHairFollow.CurrentAimPosition.x,
                    CrossHairFollow.CurrentAimPosition.y, 0));

                if (!Physics.Raycast(screenRay, out RaycastHit hit, WeaponInfo.FireDistance,
                    WeaponInfo.WeaponHitLayers)) return;
                
                WeaponInformation.BulletsInClip--;

                //Debug Hit Location
                HitLocation = hit;
                DamageTarget(hit);
            }
            //Reload weapon when out of bullets.
            else if (WeaponInformation.BulletsInClip == 0)
            {
                WeaponHolder.StartReloading();
            }
        }

        private void DamageTarget(RaycastHit hit)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(WeaponInformation.Damage);
        }

        public void StartReloading()
        {
            Reloading = true;
            ReloadWeapon();
        }

        public void StopReloading()
        {
            Reloading = false;
        }

        private void ReloadWeapon()
        {
            if (FiringEffect) Destroy(FiringEffect.gameObject);


            //If the Amount of bullets available is less than the size of the clip, add all remaining bullets.
            int bulletsToReload =  WeaponInformation.ClipSize - WeaponInformation.TotalBulletsAvailable;
            if (bulletsToReload < 0)
            {
                Debug.Log("Reload");
                WeaponInformation.BulletsInClip = WeaponInformation.ClipSize;
                WeaponInformation.TotalBulletsAvailable -= WeaponInformation.ClipSize;

            }
            else
            {
                WeaponInformation.BulletsInClip = WeaponInformation.TotalBulletsAvailable;
                WeaponInformation.TotalBulletsAvailable = 0;
            }
        }
        
        public void BeginFiringWeapon()
        {
            WeaponFiring = true;
            if (WeaponInformation.Repeating)
            {
                InvokeRepeating(nameof(FireWeapon), WeaponInformation.FireStartDelay, WeaponInformation.FireRate);
            }
            else
            {
                FireWeapon();
            }
        }

        public void EndFiringWeapon()
        {
            WeaponFiring = false;
            if (FiringEffect) Destroy(FiringEffect.gameObject);
            CancelInvoke(nameof(FireWeapon));
        }

        private void OnDrawGizmos()
        {
            if (HitLocation.transform)
            {
                Gizmos.DrawWireSphere(HitLocation.point, 0.2f);
            }
        }
    }
}