using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
    public class WeaponScriptableObject : ScriptableObject
    {
        public GameObject WeaponPrefab;
        public string Name;
        public float Damage;
        public float ClipSize;
        public float FireStartDelay;
        public float FireRate;
        public float FireDistance;
        public LayerMask WeaponHitLayer;
        
    }
}
