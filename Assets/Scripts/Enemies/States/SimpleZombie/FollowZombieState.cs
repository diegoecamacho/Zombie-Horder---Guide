using State_Machine;
using UnityEngine;

namespace Enemies
{
    public class FollowZombieState : ZombieState
    {
        private readonly GameObject FollowTarget;
        private const float StopDistance = 1f;
        private static readonly int MovementZ = Animator.StringToHash("MovementZ");
        
        public FollowZombieState(GameObject followTarget, ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
        {
            FollowTarget = followTarget;
            UpdateInterval = 1f;
        }

        public override void Enter()
        {
            base.Enter();
            Zombie.NavMeshAgent.SetDestination(FollowTarget.transform.position);
        }

        public override void IntervalUpdate()
        {
            base.IntervalUpdate();
            Zombie.NavMeshAgent.SetDestination(FollowTarget.transform.position);
        }

        public override void Update()
        {
            base.Update();
            Zombie.Animator.SetFloat(MovementZ, Zombie.NavMeshAgent.velocity.normalized.z);
            if (Vector3.Distance(Zombie.transform.position, FollowTarget.transform.position) < StopDistance)
            {
                StateMachine.ChangeState("Attack");
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
        }
    }
}