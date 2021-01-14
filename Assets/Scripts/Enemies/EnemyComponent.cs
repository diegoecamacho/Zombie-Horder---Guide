using System;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyComponent : MonoBehaviour
    {
        private NavMeshAgent NavMeshAgent;
        private GameObject PlayerGameObject;
        private Animator Animator;
        
        
        private static readonly int MovementZ = Animator.StringToHash("MovementZ");

        private void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            
        }

        public void Initialize(GameObject playerGameObject)
        {
            PlayerGameObject = playerGameObject;
        }

        private void Update()
        {
            Debug.Log(NavMeshAgent.velocity);
            NavMeshAgent.SetDestination(PlayerGameObject.transform.position);
            
            Animator.SetFloat(MovementZ, NavMeshAgent.velocity.normalized.z);
        }
    }
}
