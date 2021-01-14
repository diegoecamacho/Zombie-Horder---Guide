using System;
using UnityEngine;
using Weapons;

namespace Events
{
    public class WeaponEvents
    {
        [Serializable]
        public struct WeaponStats
        {
            public string Name;
            public float Damage;
            public int BulletsInClip;
            public int ClipSize;
            public int TotalBulletsAvailable;
            public float FireStartDelay;
            public float FireRate;
            public float FireDistance;
            public LayerMask WeaponHitLayers;
            public bool Repeating;

            public WeaponStats(string name,
                float damage,
                int bulletsInClip,
                int clipSize,
                int totalBulletsAvailable,
                float fireRate,
                float fireStartDelay,
                float fireDistance,
                bool repeating
            )
            {
                Name = name;
                Damage = damage;
                BulletsInClip = bulletsInClip;
                ClipSize = clipSize;
                FireRate = fireRate;
                FireStartDelay = fireStartDelay;
                FireDistance = fireDistance;
                TotalBulletsAvailable = totalBulletsAvailable;
                WeaponHitLayers = default;
                Repeating = repeating;
            }
        }

        public delegate void WeaponEquippedEvent(WeaponComponent weapon);
        public static event WeaponEquippedEvent OnWeaponEquipped;

        public static void Invoke_OnWeaponEquipped(WeaponComponent weapon)
        {
            OnWeaponEquipped?.Invoke(weapon);
        }
        
        public delegate void WeaponReloaded();
        public static event WeaponReloaded OnWeaponReloaded;

        public static void Invoke_OnWeaponReloaded()
        {
            OnWeaponReloaded?.Invoke();
        }
    }
}
