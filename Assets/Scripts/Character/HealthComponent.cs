using Interfaces;
using UnityEngine;

namespace Character
{
    public class HealthComponent : MonoBehaviour , IDamageable
    {

        public float Health => CurrentHealth;
        public float MaxHealth => TotalHealth;
    
        [SerializeField] private float CurrentHealth;
        [SerializeField] private float TotalHealth;
        
        protected virtual void Start()
        {
            CurrentHealth = TotalHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
        }
    }
}
