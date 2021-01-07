using Events;
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

        private bool Reloading = false;

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

        private void FireWeapon()
        {
            if (WeaponInformation.BulletsInClip > 0 && !Reloading)
            {
                Debug.Log("Fire Weapon");
                if (!FiringEffect)
                {
                    FiringEffect = Instantiate(FiringAnimation, BarrelLocation).GetComponent<ParticleSystem>();
                }

                Vector3 worldPoint = ViewCamera.ScreenToWorldPoint(new Vector3(CrossHairFollow.CurrentLookPosition.x,
                    CrossHairFollow.CurrentLookPosition.y, transform.position.z));

                AimWorldDirection = worldPoint - BarrelLocationPoint.position;
                AimWorldDirection.Normalize();

                Debug.DrawRay(BarrelLocationPoint.position, AimWorldDirection * WeaponInformation.FireDistance,
                    Color.red);
                WeaponInformation.BulletsInClip--;

                if (!Physics.Raycast(BarrelLocationPoint.position, AimWorldDirection, out RaycastHit hit,
                    WeaponInformation.FireDistance, WeaponInformation.WeaponHitLayers)) return;

                HitLocation = hit;
            }
            else if (WeaponInformation.BulletsInClip == 0)
            {
                WeaponHolder.StartReloading();
            }
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
            int bulletsToReload = WeaponInformation.TotalBulletsAvailable - WeaponInformation.ClipSize;
            if (bulletsToReload < 0)
            {
                Debug.Log("Reload - Out Of Ammo");
                WeaponInformation.BulletsInClip = WeaponInformation.TotalBulletsAvailable;
                WeaponInformation.TotalBulletsAvailable = 0;
            }
            else
            {
                Debug.Log("Reload");
                WeaponInformation.BulletsInClip = WeaponInformation.ClipSize;
                WeaponInformation.TotalBulletsAvailable -= WeaponInformation.ClipSize;
            }
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