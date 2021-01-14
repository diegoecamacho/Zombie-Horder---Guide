using Interfaces;
using UnityEngine;

namespace Character
{
    public class HealthComponent : MonoBehaviour , IDamageable, IKillable
    {

        public float Health => CurrentHealth;
    
        [SerializeField] private float CurrentHealth;
        [SerializeField] private float TotalHealth;
        
        private void Start()
        {
            CurrentHealth = TotalHealth;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                KillTarget();
            }
        }
        
        public void KillTarget()
        {
            Destroy(gameObject);
        }
    }
}
