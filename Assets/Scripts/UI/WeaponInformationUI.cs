using Events;
using TMPro;
using UnityEngine;
using Weapons;

namespace UI
{
    public class WeaponInformationUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI WeaponNameText;
        [SerializeField] private TextMeshProUGUI ClipSizeText;
        [SerializeField] private TextMeshProUGUI TotalAmmoText;

        private WeaponComponent EquippedWeapon;
        // Start is called before the first frame update
    
        private void OnWeaponEquipped(WeaponComponent weapon)
        {
            EquippedWeapon = weapon;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (!EquippedWeapon) return;
        
            WeaponNameText.text = EquippedWeapon.WeaponInfo.Name;
            ClipSizeText.text = EquippedWeapon.WeaponInfo.BulletsInClip.ToString();
            TotalAmmoText.text = EquippedWeapon.WeaponInfo.TotalBulletsAvailable.ToString();
        }
    
        private void OnEnable()
        {
            WeaponEvents.OnWeaponEquipped += OnWeaponEquipped;
        }

        private void OnDisable()
        {
            WeaponEvents.OnWeaponEquipped -= OnWeaponEquipped;
        }
    }
}
