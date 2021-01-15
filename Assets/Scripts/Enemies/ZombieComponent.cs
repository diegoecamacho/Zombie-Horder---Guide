using System;
using Character;
using Enemies.States.SimpleZombie;
using Interfaces;
using State_Machine;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(StateMachine), typeof(HealthComponent))]
    public class ZombieComponent : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent { get; private set; }
        public Animator Animator { get; private set; }
        public HealthComponent HealthComponent { get; private set; }

        public GameObject FollowTarget;
        
        protected StateMachine StateMachine;
        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            HealthComponent = GetComponent<HealthComponent>();
        }

        public void Initialize(GameObject followTarget)
        {
            FollowTarget = followTarget;
            
            StateMachine = GetComponent<StateMachine>();
            
            var idleState = new IdleZombieState(this, StateMachine);
            StateMachine.AddState("Idle", idleState);
            
            var followState = new FollowZombieState(followTarget, this, StateMachine);
            StateMachine.AddState("Follow", followState);
            
            var attackState = new AttackZombieState(followTarget, this, StateMachine);
            StateMachine.AddState("Attack", attackState);
            
            var deadState = new DeadZombieState(this, StateMachine);
            StateMachine.AddState("Dead", deadState);
            
            if (!FollowTarget)
            {
                StateMachine.Initialize("Idle");
                
            }
            else
            {
                StateMachine.Initialize("Follow");
            }
        }
    }
}
