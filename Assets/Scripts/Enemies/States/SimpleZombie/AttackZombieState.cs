using Character;
using State_Machine;
using UnityEngine;

namespace Enemies.States.SimpleZombie
{
    public class AttackZombieState : ZombieState
    {
        private GameObject FollowTarget;
        
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        private static readonly int MovementZ = Animator.StringToHash("MovementZ");
        private float StopDistance = 1f;
        
        private PlayerHealthComponent PlayerHealthComponent;

        public AttackZombieState(GameObject followTarget, ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
        {
            FollowTarget = followTarget;
            UpdateInterval = 2f;

            PlayerHealthComponent = followTarget.GetComponent<PlayerHealthComponent>();
        }
    
        public override void Enter()
        {
            Zombie.NavMeshAgent.isStopped = true;
            Zombie.NavMeshAgent.ResetPath();
            Zombie.Animator.SetFloat(MovementZ, 0);
            
            Zombie.Animator.SetBool(IsAttacking, true);
        }

        public override void IntervalUpdate()
        {
            base.IntervalUpdate();
            
            Debug.Log("Attack");
            PlayerHealthComponent.TakeDamage(10);
        }

        public override void Update()
        {
            base.Update();
            Zombie.transform.LookAt(FollowTarget.transform.position, Vector3.up);
            
            if (Vector3.Distance(Zombie.transform.position, FollowTarget.transform.position) > StopDistance)
            {
                StateMachine.ChangeState("Follow");
            }
            else if (Zombie.HealthComponent.Health <= 0)
            {
                StateMachine.ChangeState("Dead");
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            Zombie.Animator.SetBool(IsAttacking, false);
        }

   
    }
}
