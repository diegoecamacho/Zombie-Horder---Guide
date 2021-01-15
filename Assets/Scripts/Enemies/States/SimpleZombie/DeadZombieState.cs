using State_Machine;
using UnityEngine;

namespace Enemies.States.SimpleZombie
{
    public class DeadZombieState : ZombieState
    {
        private static readonly int MovementZ = Animator.StringToHash("MovementZ");
        private static readonly int IsDead = Animator.StringToHash("isDead");

        public DeadZombieState(ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
        {
        }


        public override void Enter()
        {
            base.Enter();
            Zombie.NavMeshAgent.isStopped = true;
            Zombie.NavMeshAgent.ResetPath();
            
            Zombie.Animator.SetFloat(MovementZ, 0);
            Zombie.Animator.SetBool(IsDead, true);
        }

        public override void Update()
        {
            base.Update();
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
